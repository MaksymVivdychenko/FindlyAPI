using FindlyBLL.DTOs;
using FindlyBLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FindlyAPI.Controllers;

[Route("api/books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? title,
        [FromQuery] List<string>? author, [FromQuery] string? publisher,
        [FromQuery] string? cover)
    {
        var books = await _bookService.GetAllBooks(author, title, cover, publisher);
        return Ok(books);
    }
}