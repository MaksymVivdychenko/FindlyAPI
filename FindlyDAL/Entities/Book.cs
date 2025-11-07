using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Text.Json.Nodes;

namespace FindlyDAL.Entities;

public class Book : BaseEntity
{
    public string ISBN_13 { get; set; }
    public string Title { get; set; }
    public JsonNode Author { get; set; }
    public string Publisher { get; set; }
    public string Language { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public List<Offer> Offers { get; set; }
}