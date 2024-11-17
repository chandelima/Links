using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Links.Models;

namespace Links.Data.Mapeamentos;

public class RedirectItemMap : IEntityTypeConfiguration<RedirectItem>
{
    public void Configure(EntityTypeBuilder<RedirectItem> builder)
    {
        builder.ToTable("links");

        builder.HasKey(x => x.Route);
        builder.Property(x => x.Route).HasColumnName("route").IsRequired();
        builder.Property(x => x.Redirect).HasColumnName("redirect").IsRequired();
    }
}
