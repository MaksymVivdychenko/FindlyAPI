namespace BookDataScraper;

using FindlyDAL.DB;
using FindlyDAL.Entities;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

public class BookParsingService
{
    private readonly FindlyDbContext _context;

    public BookParsingService(FindlyDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Головний метод для парсингу та збереження книги.
    /// Перевіряє ISBN; якщо книга існує, пропускає створення.
    /// </summary>
    public async Task<Book?> ParseAndSaveBookAsync(HtmlDocument doc, BookSelectors selectors)
    {
        // --- 1. Пріоритетний парсинг та обробка ISBN ---
        string isbnRaw = GetSingleNodeText(doc.DocumentNode, selectors.IsbnXPath);
        string? isbnClean = isbnRaw?.Replace("-", "");

        // Якщо ISBN не знайдено на сторінці, ми не можемо продовжити
        if (string.IsNullOrEmpty(isbnClean) || isbnClean.Contains(","))
        {
            // Або логування
            Console.WriteLine("Warning: ISBN not found on page. Skipping."); 
            return null; 
        }

        // --- 2. Пошук існуючої книги в БД ---
        var existingBook = await _context.Books
            .AsNoTracking() // Використовуємо AsNoTracking для швидкого read-only запиту
            .FirstOrDefaultAsync(b => b.ISBN_13 == isbnClean);

        // --- 3. Якщо книга існує, пропускаємо ---
        if (existingBook != null)
        {
            // Або логування
            Console.WriteLine($"Book with ISBN {isbnClean} already exists. Skipping."); 
            return existingBook; // Повертаємо існуючу книгу, щоб повідомити "цикл"
        }

        // --- 4. Книга не існує. Продовжуємо парсинг (всіх інших полів) ---
        string? coverName = GetSingleNodeText(doc.DocumentNode, selectors.CoverXPath);
        string? publisherName = GetSingleNodeText(doc.DocumentNode, selectors.PublisherXPath);
        string? titleJson = GetSingleNodeText(doc.DocumentNode, selectors.TitleJsonXPath);
        if (coverName is null || publisherName is null || titleJson is null)
        {
            Console.WriteLine("Some of required data is missing, skipping");
            return null;
        }

        if (publisherName == "Книжковий клуб \"Клуб Сімейного Дозвілля\"")
        {
            publisherName = "КСД";
        }

        // --- 5. Обробка даних згідно з правилами ---
        string bookTitle = ParseTitleFromJson(titleJson);

        // --- 6. Парсинг списку авторів ---
        List<string> authorNames = new List<string>();
        var authorNodes = doc.DocumentNode.SelectNodes(selectors.AuthorXPath);
        if (authorNodes == null)
        {
             authorNodes = doc.DocumentNode.SelectNodes(selectors.AuthorsXPath);
        }
        if (authorNodes == null)
        {
            Console.WriteLine("Author data is missed. skipping");
            return null;
        }
        
        authorNames = authorNodes
            .Select(node => node.InnerText.Trim())
            .Where(name => !string.IsNullOrEmpty(name))
            .Distinct() // Уникаємо дублікатів авторів на одній сторінці
            .ToList();
        

        // --- 7. Логіка бази даних: "Знайти або Створити" (для супутніх сутностей) ---

        // Знаходимо або створюємо Publisher
        var publisher = await GetOrCreateEntityAsync(
            _context.Publishers,
            p => p.Title == publisherName,
            new Publisher { Title = publisherName }
        );

        // Знаходимо або створюємо Cover
        var cover = await GetOrCreateEntityAsync(
            _context.Cover,
            c => c.Name == coverName,
            new Cover { Name = coverName }
        );

        // Знаходимо або створюємо Авторів (для зв'язку M-M)
        var bookAuthors = new List<Author>();
        foreach (var authorName in authorNames)
        {
            var author = await GetOrCreateEntityAsync(
                _context.Authors,
                a => a.Name == authorName,
                new Author { Name = authorName }
            );
            bookAuthors.Add(author);
        }

        // --- 8. Створення НОВОЇ Книги ---
        // Оскільки existingBook == null, ми завжди створюємо нову
        var newBook = new Book
        {
            ISBN_13 = isbnClean,
            Title = bookTitle,
            ImageUrl = null,
            Publisher = publisher,
            Cover = cover,
            Authors = bookAuthors, // EF Core автоматично створить M-M зв'язки
            Offers = new List<Offer>() // Ініціалізуємо порожній список
        };
        _context.Books.Add(newBook);


        // --- 9. Збереження ---
        // await _context.SaveChangesAsync();

        return newBook; // Повертаємо створений об'єкт
    }

    // --- Допоміжні методи ---

    /// <summary>
    /// Отримує InnerText ПЕРШОГО вузла, знайденого за XPath.
    /// </summary>
    private string GetSingleNodeText(HtmlNode docNode, string xpath)
    {
        // Використовуємо SelectNodes згідно з вимогою
        var nodes = docNode.SelectNodes(xpath);
        // Беремо перший елемент, як зазначено в правилах
        return nodes?.FirstOrDefault()?.InnerText.Trim();
    }

    /// <summary>
    /// Парсить JSON рядок і витягує значення "name".
    /// </summary>
    private string ParseTitleFromJson(string jsonString)
    {
        if (string.IsNullOrEmpty(jsonString))
        {
            return null;
        }

        try
        {
            using (JsonDocument doc = JsonDocument.Parse(jsonString))
            {
                // Намагаємося отримати властивість "name"
                if (doc.RootElement.TryGetProperty("name", out JsonElement nameElement))
                {
                    return nameElement.GetString();
                }
            }
        }
        catch (JsonException ex)
        {
            // Обробка помилки, якщо JSON некоректний
            Console.WriteLine($"JSON parsing error: {ex.Message}");
            // Можна додати логування
        }
        return null; // або повернути jsonString як є, якщо це резервний варіант
    }

    /// <summary>
    /// Узагальнений метод "Знайти або Створити" для супутніх сутностей.
    /// </summary>
    private async Task<T> GetOrCreateEntityAsync<T>(DbSet<T> dbSet, Expression<Func<T, bool>> predicate, T newEntity) where T : BaseEntity
    {
        var entity = await dbSet.FirstOrDefaultAsync(predicate);
        if (entity == null)
        {
            dbSet.Add(newEntity);
            // Важливо: ми НЕ викликаємо SaveChangesAsync() тут.
            // Ми збережемо все однією транзакцією в кінці головного методу.
            return newEntity;
        }
        return entity;
    }

    /// <summary>
    /// Оновлює M-M зв'язок авторів для існуючої книги.
    /// </summary>
    private void UpdateBookAuthors(Book existingBook, List<Author> currentAuthors)
    {
        // 1. Видаляємо авторів, яких більше немає
        var authorsToRemove = existingBook.Authors
            .Where(a => !currentAuthors.Any(ca => ca.Id == a.Id))
            .ToList();

        foreach (var author in authorsToRemove)
        {
            existingBook.Authors.Remove(author);
        }

        // 2. Додаємо нових авторів
        foreach (var author in currentAuthors)
        {
            if (!existingBook.Authors.Any(a => a.Id == author.Id))
            {
                existingBook.Authors.Add(author);
            }
        }
    }
}

public class BookSelectors
{
    public string AuthorXPath { get; set; }
    public string AuthorsXPath { get; set; }
    public string CoverXPath { get; set; }
    public string PublisherXPath { get; set; }
    public string IsbnXPath { get; set; }
    public string TitleJsonXPath { get; set; }
    public string ImagePathXPath { get; set; }
}
