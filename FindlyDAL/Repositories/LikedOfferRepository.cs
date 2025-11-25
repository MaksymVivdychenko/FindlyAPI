using FindlyDAL.DB;
using FindlyDAL.Entities;
using FindlyDAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FindlyDAL.Repositories;

public class LikedOfferRepository : Repository<UserLikedOffers>,ILikedOfferRepository
{
    public LikedOfferRepository(FindlyDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<UserLikedOffers>> GetLikedOffersByUserId(Guid userId)
    {
        return await DbContext.UserLikedOffers
            .AsNoTracking()
            .Include(q => q.Offer)
            .ThenInclude(o => o.Shop)
            .Include(q => q.Offer)
            .ThenInclude(o => o.Book)
            .ThenInclude(a => a.Authors)
            .Where(q => q.UserId == userId)
            .ToListAsync();
    }
}