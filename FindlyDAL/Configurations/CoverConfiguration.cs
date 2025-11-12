using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class CoverConfiguration : BaseConfiguration<Cover>
{
    public override void Configure(EntityTypeBuilder<Cover> builder)
    {
        base.Configure(builder);
        builder.HasData(new Cover[]
        {
            new Cover { Id = Guid.Parse("a9819fd8-65c7-4925-af38-a29c030280f6"), Name = "Тверда" },
            new Cover { Id = Guid.Parse("2a9dbeec-0c49-41e5-b947-dcd3d5b6f717"), Name = "М'яка" },
        });
    }
}