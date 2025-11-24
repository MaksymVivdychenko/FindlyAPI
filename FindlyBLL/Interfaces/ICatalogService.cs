using FindlyBLL.DTOs;
using FindlyBLL.DTOs.CatalogDtos;
using FindlyDAL.Entities;

namespace FindlyBLL.Interfaces;

public interface ICatalogService
{
    Task<List<CoverGetDto>> GetAllCovers();
    Task<List<PublisherGetDto>> GetAllPublishers();
    Task<List<BookGetDto>> GetAllBooks(BookFilterDto bookFilterDto);
}