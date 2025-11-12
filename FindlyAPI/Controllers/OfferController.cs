using FindlyBLL.Services;
using FindlyDAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FindlyAPI.Controllers;

public class OfferController : ControllerBase
{
    private readonly OfferService _offerService;
    public OfferController(OfferService offerService)
    {
        this._offerService = offerService;
    }
}
/**/