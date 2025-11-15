using System.Reflection.Metadata.Ecma335;
using System.Runtime.Serialization;
using System.Text.Json.Nodes;

namespace FindlyDAL.Entities;

public class Book : BaseEntity
{
    public string ISBN_13 { get; set; }
    public string Title { get; set; }
    public string? ImageUrl { get; set; }
    public List<Offer> Offers { get; set; }
    public List<Author> Authors { get; set; }
    
    public Guid PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    
    public Guid CoverId { get; set; }
    public Cover Cover { get; set; }
}