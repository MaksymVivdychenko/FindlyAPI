using FindlyBLL.DTOs;
using FindlyBLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindlyAPI.Controllers;

[Route("api/[controller]")]
[Authorize]
public class FavoritesController : ControllerBase
{
    private readonly IOfferService _offerService;

    public FavoritesController(IOfferService offerService)
    {
        this._offerService = offerService;
    }

    [HttpPost]
    public async Task<IActionResult> AddOfferToLiked([FromBody] AddOfferToLikedDto offerToLikedDto)
    {
        await _offerService.AddOfferToFavorite(offerToLikedDto.userId, offerToLikedDto.offerId);
        return StatusCode(204);
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveOfferFromLiked([FromBody] AddOfferToLikedDto offerToLikedDto)
    {
        await _offerService.AddOfferToFavorite(offerToLikedDto.userId, offerToLikedDto.offerId);
        return StatusCode(204);
    }
    [HttpPatch]
    public async Task<IActionResult> AddPriceToNotify([FromBody] AddOfferToLikedDto offerToLikedDto)
    {
        await _offerService.AddOfferToFavorite(offerToLikedDto.userId, offerToLikedDto.offerId);
        return StatusCode(204);
    }
    [HttpPatch]
    public async Task<IActionResult> RemovePriceToNotify([FromBody] AddOfferToLikedDto offerToLikedDto)
    {
        await _offerService.AddOfferToFavorite(offerToLikedDto.userId, offerToLikedDto.offerId);
        return StatusCode(204);
    }
}