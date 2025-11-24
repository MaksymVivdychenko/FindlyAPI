using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(FindlyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Cover>> GetAllCovers()
    {
        return await DbContext.Cover.ToListAsync();
    }

    public async Task<IEnumerable<Publisher>> GetAllPublishers()
    {
        return await DbContext.Publishers.ToListAsync();
    }

    public async Task<IEnumerable<Book>> FindBooksByData(string? bookTitle, Guid? coverId, Guid? publisherId, string? author,
        int pageSize, int pageNumber, bool isAvailable)
    {
        IQueryable<Book> query = DbContext.Books
            .Include(q => q.Authors)
            .Include( q=> q.Publisher)
            .Include(q => q.Cover)
            .Include(q => q.Offers);
        if (isAvailable)
        {
            query = query.Where(b => b.Offers.Any(q => q.IsAvailable));
        }
        if (!String.IsNullOrEmpty(author))
        {
            query = query.Where(b => b.Authors.Any(q => q.Name.Contains(author)));
        }

        if (!String.IsNullOrEmpty(bookTitle))
        {
            query = query.Where(q => q.Title.ToLower().Contains(bookTitle.ToLower()));
        }

        if (coverId != null)
        {
            query = query.Where(q => q.Cover.Id == coverId);
        }

        if (publisherId != null)
        {
            query = query.Where(q => q.Publisher.Id == publisherId);
        }
        
        return await query.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
    }
}