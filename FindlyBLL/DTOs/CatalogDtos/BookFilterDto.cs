namespace FindlyBLL.DTOs.CatalogDtos;

public class BookFilterDto
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public Guid? PublisherId { get; set; }
    public Guid? CoverId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool IsAvailable { get; set; } = false;
}