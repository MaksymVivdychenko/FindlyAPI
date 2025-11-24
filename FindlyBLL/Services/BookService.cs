using FindlyBLL.DTOs;
using FindlyBLL.Interfaces;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FindlyBLL.Services;

public class BookService : IBookService
{
    private readonly FindlyDbContext _context;

    public BookService(FindlyDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookGetDto>> GetAllBooks(List<string>? authors, string? bookTitle, string? cover, string? publisher)
    {
        IQueryable<Book> query = _context.Books
            .Include(q => q.Authors)
            .Include( q=> q.Publisher)
            .Include(q => q.Cover);
        if (authors is not null && authors.Any())
        {
            query = query.Where(book => 
                book.Authors.Any(author => authors.Contains(author.Name)));
        }

        if (!String.IsNullOrEmpty(bookTitle))
        {
            query = query.Where(q => q.Title.ToLower().Contains(bookTitle.ToLower()));
        }

        if (!String.IsNullOrEmpty(cover))
        {
            query = query.Where(q => q.Cover.Name.ToLower() == cover.ToLower());
        }

        if (!String.IsNullOrEmpty(publisher))
        {
            query = query.Where(q => q.Publisher.Title.ToLower() == publisher.ToLower());
        }

        return await query.Select(q => new BookGetDto
        {
            Id = q.Id,
            Cover = q.Cover.Name,
            Publisher = q.Publisher.Title,
            Title = q.Title,
            Author = q.Authors.Select(a => a.Name).ToList(),
        })
        .ToListAsync();
    }
}