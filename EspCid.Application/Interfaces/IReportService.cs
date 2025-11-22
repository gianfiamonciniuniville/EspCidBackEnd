using System.Collections.Generic;
using System.Threading.Tasks;
using EspCid.Application.DTO;

namespace EspCid.Application.Interfaces;

public interface IReportService
{
    Task<ReportDto> CreateReportAsync(int userId, CreateReportDto createReportDto);
    Task<ReportDto> UpdateReportAsync(int reportId, UpdateReportDto updateReportDto);
    Task<ReportDto?> GetReportByIdAsync(int reportId);
    Task<IEnumerable<ReportDto>> GetAllReportsAsync();
    Task<IEnumerable<ReportDto>> GetReportsByCityAsync(string city);
    Task<IEnumerable<ReportDto>> GetReportsByUserAsync(int userId);
    Task<IEnumerable<byte[]>> GetReportPhotosAsync(int reportId);
}