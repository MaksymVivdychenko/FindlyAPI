using FindlyDAL.Entities;

namespace FindlyDAL.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task<IEnumerable<Cover>> GetAllCovers();
    Task<IEnumerable<Publisher>> GetAllPublishers();
    Task<IEnumerable<Book>> FindBooksByData(string? bookTitle, Guid? coverId, Guid? publisherId, string? author,
        int pageSize, int pageNumber, bool isAvailable);
}