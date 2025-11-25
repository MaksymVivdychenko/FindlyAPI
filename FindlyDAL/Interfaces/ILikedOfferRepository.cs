using FindlyDAL.Entities;

namespace FindlyDAL.Interfaces;

public interface ILikedOfferRepository : IRepository<UserLikedOffers>
{
    Task<IEnumerable<UserLikedOffers>> GetLikedOffersByUserId(Guid userId);
}