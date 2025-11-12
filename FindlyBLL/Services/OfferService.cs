using System.Xml;
using FindlyDAL.DB;
using FindlyDAL.Entities;

namespace FindlyBLL.Services;

public class OfferService
{
    private readonly FindlyDbContext _context;

    public OfferService(FindlyDbContext context)
    {
        _context = context;
    }

    public List<Offer> GetAllOffers()
    {
        return _context.Offers.ToList();
    }
}