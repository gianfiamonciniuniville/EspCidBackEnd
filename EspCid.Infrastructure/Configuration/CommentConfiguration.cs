using EspCid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EspCid.Infrastructure.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable(nameof(Comment));
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.Content).IsRequired();

        builder.HasOne(c => c.Report)
            .WithMany()
            .HasForeignKey(c => c.ReportId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete for user
        
        builder.HasData(
            new Comment
            {
                Id = 1,
                Content = "Ótima iniciativa! Espero que resolvam logo este buraco.",
                ReportId = 1,
                UserId = 2,
                Created = new DateTime(2023, 1, 1, 10, 0, 0),
                Updated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            },
            new Comment
            {
                Id = 2,
                Content = "Concordo! Já causou acidentes aqui.",
                ReportId = 1,
                UserId = 1,
                Created = new DateTime(2023, 1, 1, 10, 30, 0),
                Updated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            },
            new Comment
            {
                Id = 3,
                Content = "A prefeitura já foi notificada sobre este problema.",
                ReportId = 2,
                UserId = 1,
                Created = new DateTime(2023, 1, 2, 14, 0, 0),
                Updated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            },
            new Comment
            {
                Id = 4,
                Content = "Realmente, o lixo está acumulando e atraindo pragas.",
                ReportId = 2,
                UserId = 2,
                Created = new DateTime(2023, 1, 2, 14, 45, 0),
                Updated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            },
            new Comment
            {
                Id = 5,
                Content = "Espero que o problema do poste seja resolvido rapidamente.",
                ReportId = 3,
                UserId = 2,
                Created = new DateTime(2023, 1, 3, 9, 0, 0),
                Updated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            }
        );
    }
}