namespace FindlyBLL.DTOs.OffersDto;

public class AddPriceToOffer
{
    public Guid OfferId { get; set; }   
    public decimal Price { get; set; }
}