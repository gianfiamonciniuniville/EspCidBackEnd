using EspCid.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EspCid.Infrastructure;

public class EspCidDbContext: DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<CommentPhoto> CommentPhotos { get; set; }
    
    public EspCidDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EspCidDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }   
}