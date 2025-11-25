using AutoMapper;
using FindlyBLL.DTOs;
using FindlyBLL.DTOs.CatalogDtos;
using FindlyBLL.Interfaces;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FindlyBLL.Services;

public class CatalogService : ICatalogService
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public CatalogService(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<List<CoverGetDto>> GetAllCovers()
    {
        var covers = (await _bookRepository.GetAllCovers()).ToList();
        return _mapper.Map<List<CoverGetDto>>(covers);
    }

    public async Task<List<PublisherGetDto>> GetAllPublishers()
    {
        var publishers = (await _bookRepository.GetAllPublishers()).ToList();
        return _mapper.Map<List<PublisherGetDto>>(publishers);
    }

    public async Task<List<BookGetDto>> GetAllBooks(BookFilterDto bookFilterDto)
    {
        var booksEntity =  (await _bookRepository.FindBooksByData(bookFilterDto.Title,
            bookFilterDto.CoverId, bookFilterDto.PublisherId,
            bookFilterDto.Author, bookFilterDto.PageSize,
            bookFilterDto.PageNumber, bookFilterDto.IsAvailable)).ToList();
        return _mapper.Map<List<BookGetDto>>(booksEntity);
    }
}