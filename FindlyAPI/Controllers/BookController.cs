using FindlyBLL.DTOs;
using FindlyBLL.DTOs.CatalogDtos;
using FindlyBLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FindlyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet("books")]
    public async Task<IActionResult> GetAllBooksAsync([FromQuery] BookFilterDto bookFilterDto)
    {
        var books = await _catalogService.GetAllBooks(bookFilterDto);
        return Ok(books);
    }
    
    [HttpGet("covers")]
    public async Task<IActionResult> GetAllCoversAsync()
    {
        var covers = await _catalogService.GetAllCovers();
        return Ok(covers);
    }
    
    [HttpGet("publishers")]
    public async Task<IActionResult> GetAllPublishersAsync()
    {
        var publishers = await _catalogService.GetAllPublishers();
        return Ok(publishers);
    }
}