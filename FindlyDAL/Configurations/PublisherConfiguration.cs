using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class PublisherConfiguration : BaseConfiguration<Publisher>
{
    public override void Configure(EntityTypeBuilder<Publisher> builder)
    {
        base.Configure(builder);
    }
}