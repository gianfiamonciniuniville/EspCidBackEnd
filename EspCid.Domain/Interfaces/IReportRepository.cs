using System.Collections.Generic;
using System.Threading.Tasks;
using EspCid.Domain.Entities;

namespace EspCid.Domain.Interfaces;

public interface IReportRepository
{
    Task<Report> CreateAsync(Report report);
    Task<Report> UpdateAsync(Report report);
    Task<Report?> GetByIdAsync(int id);
    Task<IEnumerable<Report>> GetAllAsync();
    Task<IEnumerable<Report>> GetByCityAsync(string city);
    Task<IEnumerable<Report>> GetByUserAsync(int userId);
}
