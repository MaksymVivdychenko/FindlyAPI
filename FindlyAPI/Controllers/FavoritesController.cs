using System.Security.Claims;
using FindlyBLL.DTOs;
using FindlyBLL.DTOs.OffersDto;
using FindlyBLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindlyAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FavoritesController : ControllerBase
{
    private readonly IOfferService _offerService;

    public FavoritesController(IOfferService offerService)
    {
        this._offerService = offerService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetLikedOffersForUser()
    {
        Guid userId = GetUserIdFromToken();
        var likedOffers = await _offerService.GetLikedOfferForUser(userId);
        return Ok(likedOffers);
    }
    [HttpPost("{offerId:guid}")]
    public async Task<IActionResult> AddOfferToLiked(Guid offerId)
    {
        Guid userId = GetUserIdFromToken();
        await _offerService.AddOfferToFavorite(userId, offerId);
        return Ok(new {message = "offer successfully added"});
    }
    
    [HttpDelete("{offerId:guid}")]
    public async Task<IActionResult> RemoveOfferFromLiked(Guid offerId)
    {
        Guid userId = GetUserIdFromToken();
        await _offerService.RemoveOfferFromFavorite(userId, offerId);
        return Ok(new {message = "offer successfully deleted"});
    }
    
    [HttpPatch("add-price")]
    public async Task<IActionResult> AddPriceToNotify([FromBody] AddPriceToOffer priceToOffer)
    {
        Guid userId = GetUserIdFromToken();
        await _offerService.AddPriceToNotify(userId, priceToOffer.OfferId, priceToOffer.Price);
        return Ok(new {message = "offer successfully updated"});
    }
    [HttpPatch("remove-price/{offerId:guid}")]
    public async Task<IActionResult> RemovePriceToNotify(Guid offerId)
    {
        Guid userId = GetUserIdFromToken();
        await _offerService.RemoveNotify(userId, offerId);
        return Ok(new {message = "offer successfully updated"});
    }

    private Guid GetUserIdFromToken()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        return Guid.Parse(id);
    }
}