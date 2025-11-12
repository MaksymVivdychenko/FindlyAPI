using FindlyBLL.DTOs;

namespace FindlyBLL.Interfaces;

public interface IBookService
{
    List<BookGetDto> GetAllBooks(List<string>? authors, string? bookTitle, string? cover, string? publisher);
}