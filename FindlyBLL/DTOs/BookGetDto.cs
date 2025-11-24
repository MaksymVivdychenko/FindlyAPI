namespace FindlyBLL.DTOs;

public class BookGetDto
{
    public Guid Id { get; set; }
    public List<string> Author { get; set; }
    public string Title { get; set; }
    public string Cover { get; set; }
    public string Publisher { get; set; }
}