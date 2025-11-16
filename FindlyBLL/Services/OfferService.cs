using System.Xml;
using FindlyBLL.Interfaces;
using FindlyDAL.DB;
using FindlyDAL.Entities;

namespace FindlyBLL.Services;

public class OfferService : IOfferService
{
    private readonly FindlyDbContext _context;

    public OfferService(FindlyDbContext context)
    {
        _context = context;
    }

    public List<Offer> GetOffersByBookId(Guid bookId)
    {
        return _context.Offers.Where(q => q.BookId == bookId).ToList();
    }

    public void AddOfferToFavorite(Guid userId, Guid offerId)
    {
        var offer = _context.Offers.Find(offerId);
        var user = _context.Users.Find(userId) !;
        UserLikedOffers likedOffer = new UserLikedOffers { OfferId = offerId, UserId = userId };
        user.LikedOffers.Add(likedOffer);
        _context.SaveChanges();
    }

    public void RemoveOfferFromFavorite(Guid userId, Guid offerId)
    {
        var likedOffer = _context.UserLikedOffers.Find(userId, offerId) !;
        _context.UserLikedOffers.Remove(likedOffer);
        _context.SaveChanges();
    }

    public void AddPriceToNotify(Guid userId, Guid offerId, decimal price)
    {
        var likedOffer = _context.UserLikedOffers.Find(userId, offerId) !;
        likedOffer.PriceToNotify = price;
        _context.SaveChanges();
    }

    public void RemoveNotify(Guid userId, Guid offerId, decimal price)
    {
        throw new NotImplementedException();
    }
}