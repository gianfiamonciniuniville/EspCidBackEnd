using EspCid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EspCid.Infrastructure.Configuration;

public class CommentPhotoConfiguration : IEntityTypeConfiguration<CommentPhoto>
{
    public void Configure(EntityTypeBuilder<CommentPhoto> builder)
    {
        builder.ToTable(nameof(CommentPhoto));
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.ImageData).IsRequired();

        builder.HasOne(cp => cp.Comment)
            .WithMany(c => c.Photos)
            .HasForeignKey(cp => cp.CommentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}