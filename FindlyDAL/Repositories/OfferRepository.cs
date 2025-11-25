using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FindlyDAL.Repositories;

public class OfferRepository : Repository<Offer>, IOfferRepository
{
    public OfferRepository(FindlyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Offer>> GetOffersForBook(Guid bookId)
    {
         return await DbContext.Offers.Include(q => q.LikedOffers)
             .Include(q => q.Shop)
             .Where(q => q.Book.Id == bookId).ToListAsync();
    }
}