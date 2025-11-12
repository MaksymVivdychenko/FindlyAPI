namespace FindlyBLL.DTOs;

public class BookGetDto
{
    public List<string> Author { get; set; }
    public string Title { get; set; }
    public string Cover { get; set; }
    public string Publisher { get; set; }
}