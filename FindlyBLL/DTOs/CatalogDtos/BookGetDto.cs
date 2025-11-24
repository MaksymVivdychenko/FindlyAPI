namespace FindlyBLL.DTOs.CatalogDtos;

public class BookGetDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Authors { get; set; }
    public string Publisher { get; set; }
    public string Cover { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool IsAvailable { get; set; }
}