using FindlyDAL.Entities;

namespace FindlyBLL.Interfaces;

public interface IOfferService
{
    List<Offer> GetOffersByBookId(Guid bookId);
    void AddOfferToFavorite(Guid userId, Guid offerId);
    void RemoveOfferFromFavorite(Guid userId, Guid offerId);
    void AddPriceToNotify(Guid userId, Guid offerId, decimal price);
    void RemoveNotify(Guid userId, Guid offerId, decimal price);
}