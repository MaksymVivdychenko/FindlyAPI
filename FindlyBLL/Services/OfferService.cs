using System.Xml;
using FindlyBLL.DTOs;
using FindlyBLL.Interfaces;
using FindlyDAL.DB;
using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindlyBLL.Services;

public class OfferService : IOfferService
{
    private readonly FindlyDbContext _context;

    public OfferService(FindlyDbContext context)
    {
        _context = context;
    }

    public async Task<List<OfferGetDto>> GetOffersByBookId(Guid bookId)
    {
        return await _context.Offers.Include(q => q.Shop).Where(q => q.BookId == bookId).Select(q => new OfferGetDto
        {
            Id = q.Id,
            Link = q.Link,
            Price = q.Price,
            ShopName = q.Shop.Name,
        }).ToListAsync();
    }

    public async Task AddOfferToFavorite(Guid userId, Guid offerId)
    {
        var offer = await _context.Offers.FindAsync(offerId);
        if (offer == null)
        {
            throw new Exception("offer doesn't exist");
        }
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            throw new Exception("user doesn't exist");
        }
        UserLikedOffers likedOffer = new UserLikedOffers { OfferId = offerId, UserId = userId };
        await _context.UserLikedOffers.AddAsync(likedOffer);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveOfferFromFavorite(Guid userId, Guid offerId)
    {
        var likedOffer = await _context.UserLikedOffers.FindAsync(userId, offerId) !;
        _context.UserLikedOffers.Remove(likedOffer!);
        await _context.SaveChangesAsync();
    }

    public async Task AddPriceToNotify(Guid userId, Guid offerId, decimal price)
    {
        var likedOffer = await _context.UserLikedOffers.FindAsync(userId, offerId) !;
        likedOffer!.PriceToNotify = price;
        await _context.SaveChangesAsync();
    }

    public async Task RemoveNotify(Guid userId, Guid offerId, decimal price)
    {
        throw new NotImplementedException();
    }
}