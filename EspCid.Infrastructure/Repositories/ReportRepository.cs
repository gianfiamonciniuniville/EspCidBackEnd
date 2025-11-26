using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EspCid.Domain.Entities;
using EspCid.Domain.Interfaces;
using EspCid.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EspCid.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly EspCidDbContext _context;

    public ReportRepository(EspCidDbContext context)
    {
        _context = context;
    }

    public async Task<Report> CreateAsync(Report report)
    {
        report.Created = DateTime.UtcNow;
        await _context.Reports.AddAsync(report);
        await _context.SaveChangesAsync();
        return report;
    }

    public async Task<IEnumerable<Report>> GetAllAsync()
    {
        return await _context.Reports
            .Include(r => r.User)
            .Include(r => r.Photos)
            .ToListAsync();
    }

    public async Task<Report?> GetByIdAsync(int id)
    {
        return await _context.Reports
            .Include(r => r.User)
            .Include(r => r.Photos)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Report>> GetByCityAsync(string city)
    {
        return await _context.Reports
            .Include(r => r.User)
            .Include(r => r.Photos)
            .Where(r => r.Localization.Contains(city))
            .ToListAsync();
    }

    public async Task<IEnumerable<Report>> GetByUserAsync(int userId)
    {
        return await _context.Reports
            .Include(r => r.User)
            .Include(r => r.Photos)
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }

    public async Task<Report?> GetReportAtLocationAsync(double latitude, double longitude)
    {
        return await _context.Reports
            .FirstOrDefaultAsync(r => r.Latitude == latitude && r.Longitude == longitude);
    }

    public async Task<Report> UpdateAsync(Report report)
    {
        report.Updated = DateTime.UtcNow;
        _context.Reports.Update(report);
        await _context.SaveChangesAsync();
        return report;
    }
}
