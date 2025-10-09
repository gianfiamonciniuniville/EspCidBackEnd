using Microsoft.EntityFrameworkCore;

namespace EspCid.Infrastructure;

public class EspCidDbContext: DbContext
{
    
    
    public EspCidDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EspCidDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }   
}