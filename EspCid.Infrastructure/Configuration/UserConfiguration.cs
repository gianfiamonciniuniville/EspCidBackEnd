using EspCid.Domain;
using EspCid.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EspCid.Infrastructure.Configuration;

public class UserConfiguration(): IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();
        builder.Property(p => p.FirstName).IsRequired();
        builder.Property(p => p.LastName).IsRequired();
        builder.Property(p => p.Document).IsRequired();
        builder.Property(p => p.Phone).IsRequired();
        builder.Property(p => p.Email).IsRequired();
        builder.Property(p => p.Bio);
        builder.Property(p => p.PasswordHash).IsRequired();
        builder.Property(p => p.ProfileImageUrl);
        builder.Property(p => p.Role).HasDefaultValue(nameof(Role.Autor));
        
        builder.HasData([
            new User() { Id = 1, Email = "user1@user.com", Bio = "Test bio user 1", PasswordHash = "Teste123", ProfileImageUrl = "", Role = nameof(Role.Autor), Document = "12345678901", FirstName = "Test", LastName = "User", Phone = "1234567890" }
        ]);
    }
}