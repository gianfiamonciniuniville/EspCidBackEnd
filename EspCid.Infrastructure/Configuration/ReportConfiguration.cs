using EspCid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EspCid.Infrastructure.Configuration;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable(nameof(Report));
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Localization).IsRequired();
        builder.Property(p => p.Latitude); // Allow null
        builder.Property(p => p.Longitude); // Allow null
        builder.Property(p => p.Status).IsRequired();

        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId);

        builder.HasMany(r => r.Photos)
            .WithOne(p => p.Report)
            .HasForeignKey(p => p.ReportId);

        builder.HasData(
            new Report
            {
                Id = 1,
                Title = "Buraco na rua",
                Description = "Buraco na rua em frente ao número 123",
                Localization = "Rua das Flores, 123, Bairro Jardim, Joinville, Santa Catarina",
                Latitude = -26.3044,
                Longitude = -48.8477,
                Created = new DateTime(2023, 1, 1),
                Status = Domain.Enums.ReportStatus.EmAberto,
                UserId = 1
            },
            new Report
            {
                Id = 2,
                Title = "Lixo na calçada",
                Description = "Lixo acumulado na calçada da rua X",
                Localization = "Avenida Brasil, 456, Bairro Centro, Joinville, Santa Catarina",
                Latitude = -26.3000,
                Longitude = -48.8500,
                Created = new DateTime(2023, 1, 2),
                Status = Domain.Enums.ReportStatus.EmAndamento,
                UserId = 2
            },
            new Report
            {
                Id = 3,
                Title = "Poste de luz queimado",
                Description = "Poste de luz queimado na praça Y",
                Localization = "Rua Principal, 789, Bairro Itinga, Araquari, Santa Catarina",
                Latitude = -26.3100,
                Longitude = -48.8400,
                Created = new DateTime(2023, 1, 3),
                Status = Domain.Enums.ReportStatus.Fechado,
                UserId = 1
            }
        );
    }
}
