namespace FindlyDAL.Entities;

public class Cover : BaseEntity
{
    private string Name { get; set; }
    public List<Book> Books { get; set; }
}