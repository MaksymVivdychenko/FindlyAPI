namespace FindlyBLL.DTOs.OffersDto;

public class LikedOfferDto
{
    public Guid OfferId { get; set; }
    
    public string BookTitle { get; set; }
    public string BookImageUrl { get; set; }
    public List<string> Authors { get; set; }
    
    public string ShopName { get; set; }
    public string Link { get; set; }
    public decimal CurrentPrice { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsNotifySet { get; set; }
}