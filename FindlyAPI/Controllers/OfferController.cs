using FindlyBLL.DTOs;
using FindlyBLL.Interfaces;
using FindlyBLL.Services;
using FindlyDAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FindlyAPI.Controllers;

[Route("api/offers")]
[ApiController]
public class OfferController : ControllerBase
{
    private readonly IOfferService _offerService;
    public OfferController(IOfferService offerService)
    {
        this._offerService = offerService;
    }
    
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetOffersByBookId([FromRoute]Guid id)
    {
        var offers = await _offerService.GetOffersByBookId(id);
        return Ok(offers);
    }

   [HttpPost("liked/")]
    public async Task<IActionResult> AddOfferToLiked([FromBody] AddOfferToLikedDto offerToLikedDto)
    {
        await _offerService.AddOfferToFavorite(offerToLikedDto.userId, offerToLikedDto.offerId);
        return StatusCode(204);
    }
}
/**/