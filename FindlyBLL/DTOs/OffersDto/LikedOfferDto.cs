namespace FindlyBLL.DTOs.OffersDto;

public class LikedOfferDto
{
    public Guid OfferId { get; set; }
    public Guid BookId { get; set; }
    
    public string BookTitle { get; set; }
    public string BookImageUrl { get; set; }
    public string Authors { get; set; }

    // Інфо про ціну та магазин
    public string ShopName { get; set; }
    public string ShopLogoUrl { get; set; }
    public decimal CurrentPrice { get; set; }
    public bool IsAvailable { get; set; }
        
    // Налаштування користувача (дзвіночок)
    public decimal? PriceToNotify { get; set; }
}