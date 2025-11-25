namespace FindlyBLL.DTOs.OffersDto;

public class OfferDto
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string Link { get; set; }
    public string ShopName { get; set; }
    public string ShopLogoUrl { get; set; }
    public bool IsLiked { get; set; }
    public bool IsPriceSet { get; set; }
}