namespace FindlyDAL.Entities;

public class Cover : BaseEntity
{
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}