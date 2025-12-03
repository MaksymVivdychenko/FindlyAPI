namespace BookDataScraper;

public class ImageService
{
    private readonly HttpClient _httpClient;
    // Шлях краще брати з конфігурації appsettings.json, але для прикладу - константа
    private const string BaseStoragePath = @"C:\study\5 sem\CourseWork\Findly\FindlyAPI\FindlyAPI\wwwroot\images\";

    public ImageService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string?> DownloadAndSaveImageAsync(string imageUrl, string isbn)
    {
        if (string.IsNullOrEmpty(imageUrl)) return null;

        try
        {
            // Нормалізація URL (додавання хоста для KSD, якщо треба)
            if (!imageUrl.StartsWith("http"))
            {
                // Тут можна додати логіку визначення хоста, або передавати повний URL
                // Для простоти припускаємо, що парсер вже повернув повний URL, 
                // або обробляємо це на рівні парсера.
            }

            var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
            var extension = Path.GetExtension(new Uri(imageUrl).AbsolutePath);
            
            // Генеруємо ім'я файлу
            var newFileName = $"{isbn}_image{extension}";
            var fullPath = Path.Combine(BaseStoragePath, newFileName);

            // Переконуємось, що папка існує
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            await File.WriteAllBytesAsync(fullPath, imageBytes);

            return $"/images/{newFileName}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading image for ISBN {isbn}: {ex.Message}");
            return null;
        }
    }
}