using FindlyBLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace FindlyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly OfferService _offerService;
    public UserController(OfferService offerService)
    {
        this._offerService = offerService;
    }
}