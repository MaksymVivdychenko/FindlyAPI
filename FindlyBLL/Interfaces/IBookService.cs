using FindlyBLL.DTOs;

namespace FindlyBLL.Interfaces;

public interface IBookService
{
    Task<List<BookGetDto>> GetAllBooks(List<string>? authors, string? bookTitle, string? cover, string? publisher);
}