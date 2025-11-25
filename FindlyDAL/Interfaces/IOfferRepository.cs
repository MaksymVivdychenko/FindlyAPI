using FindlyDAL.Entities;

namespace FindlyDAL.Interfaces;

public interface IOfferRepository : IRepository<Offer>
{
    Task<IEnumerable<Offer>> GetOffersForBook(Guid bookId);
}