using FindlyBLL.DTOs;
using FindlyBLL.DTOs.OffersDto;
using FindlyDAL.Entities;

namespace FindlyBLL.Interfaces;

public interface IOfferService
{
    Task<List<OfferDto>> GetOffersByBookId(Guid bookId, Guid? userId);
    Task AddOfferToFavorite(Guid userId, Guid offerId);
    Task RemoveOfferFromFavorite(Guid userId, Guid offerId);
    Task AddPriceToNotify(Guid userId, Guid offerId, decimal price);
    Task RemoveNotify(Guid userId, Guid offerId);
}