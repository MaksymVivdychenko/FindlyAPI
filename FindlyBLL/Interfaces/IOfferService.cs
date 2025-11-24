using FindlyBLL.DTOs;
using FindlyDAL.Entities;

namespace FindlyBLL.Interfaces;

public interface IOfferService
{
    Task<List<OfferGetDto>> GetOffersByBookId(Guid bookId);
    Task AddOfferToFavorite(Guid userId, Guid offerId);
    Task RemoveOfferFromFavorite(Guid userId, Guid offerId);
    Task AddPriceToNotify(Guid userId, Guid offerId, decimal price);
    Task RemoveNotify(Guid userId, Guid offerId, decimal price);
}