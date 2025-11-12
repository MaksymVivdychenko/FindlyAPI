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

    public List<BookGetDto> GetAllBooks(List<string>? authors, string? bookTitle, string? cover, string? publisher)
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

        if (!bookTitle.IsNullOrEmpty())
        {
            query = query.Where(q => q.Title.ToLower() == bookTitle.ToLower());
        }

        if (!cover.IsNullOrEmpty())
        {
            query = query.Where(q => q.Cover.Name.ToLower() == cover.ToLower());
        }

        if (!publisher.IsNullOrEmpty())
        {
            query = query.Where(q => q.Publisher.Title.ToLower() == publisher.ToLower());
        }

        return query.Select(q => new BookGetDto
        {
            Cover = q.Cover.Name,
            Publisher = q.Publisher.Title,
            Title = q.Title,
            Author = q.Authors.Select(a => a.Name).ToList(),
        })
        .ToList();
    }
}