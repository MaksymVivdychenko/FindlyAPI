using FindlyDAL.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FindlyDAL.Configurations;

public class PublisherConfiguration : BaseConfiguration<Publisher>
{
    public override void Configure(EntityTypeBuilder<Publisher> builder)
    {
        base.Configure(builder);
        builder.HasData(new[]
        {
            new Publisher { Id = Guid.Parse("42e470a1-702a-4994-a7f9-0b7283cc41d6"), Title = "Видавництво Жупанського" },
            new Publisher { Id = Guid.Parse("0a20441e-d50a-4da4-b560-a91c4aa69080"), Title = "BookChef" },
            new Publisher { Id = Guid.Parse("07a4509b-9e0d-4cff-9ebb-8a7936f2dc4a"), Title = "КСД" },
        });
    }
}