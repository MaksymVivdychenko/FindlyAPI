using System.Security.Claims;
using FindlyBLL.Interfaces;
using FindlyBLL.Services;
using FindlyDAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FindlyAPI.Controllers;

[Route("api/books/{bookId}/offers")]
[ApiController]
public class OfferController : ControllerBase
{
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        this._offerService = offerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOffersByBookId(Guid bookId)
    {
        Guid? userId = null;
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null)
        {
            userId = Guid.Parse(userIdClaim.Value);
        }

        var offers = await _offerService.GetOffersByBookId(bookId, userId);
        return Ok(offers);
    }
}