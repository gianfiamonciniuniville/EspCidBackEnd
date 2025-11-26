using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EspCid.Application.DTO;
using EspCid.Application.Interfaces;
using System.IO;
using EspCid.Domain.Entities;
using EspCid.Domain.Enums;
using EspCid.Domain.Interfaces;

namespace EspCid.Application.Services;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;
    private readonly IUserRepository _userRepository;

    public ReportService(IReportRepository reportRepository, IUserRepository userRepository)
    {
        _reportRepository = reportRepository;
        _userRepository = userRepository;
    }

    public async Task<ReportDto> CreateReportAsync(CreateReportDto createReportDto)
    {
        var user = await _userRepository.GetByIdAsync(createReportDto.UserId) 
                   ?? throw new Exception("User not found.");
        
        if (createReportDto.Latitude.HasValue && createReportDto.Longitude.HasValue)
        {
            var existingReport = await _reportRepository.GetReportAtLocationAsync(
                createReportDto.Latitude.Value, 
                createReportDto.Longitude.Value);

            if (existingReport != null)
            {
                throw new Exception("Já existe um relato nesta localização.");
            }
        }

        var report = new Report
        {
            Title = createReportDto.Title,
            Description = createReportDto.Description,
            Localization = createReportDto.Localization,
            Latitude = createReportDto.Latitude,
            Longitude = createReportDto.Longitude,
            Status = ReportStatus.EmAberto,
            UserId = createReportDto.UserId,
        };

        foreach (var photo in createReportDto.Photos)
        {
            using var memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);
            var imageDataString = Convert.ToBase64String(memoryStream.ToArray());
            var imageDataPrefix = "data:image/jpeg;base64,";
            if (!imageDataString.StartsWith(imageDataPrefix))
                imageDataString = imageDataPrefix + imageDataString;
            report.Photos.Add(new Photo { ImageData = System.Text.Encoding.UTF8.GetBytes(imageDataString) });
        }
        await _reportRepository.CreateAsync(report);

        // This is not ideal, as the user object is not loaded yet.
        // We should load it before creating the DTO.
        report.User = user;

        return new ReportDto(report);
    }

    public async Task<bool> ReportExistsAtLocationAsync(double latitude, double longitude)
    {
        return await _reportRepository.GetReportAtLocationAsync(latitude, longitude) != null;
    }

    public async Task<IEnumerable<ReportDto>> GetAllReportsAsync()
    {
        var reports = await _reportRepository.GetAllAsync();
        return reports.Select(r => new ReportDto(r));
    }

    public async Task<ReportDto?> GetReportByIdAsync(int reportId)
    {
        var report = await _reportRepository.GetByIdAsync(reportId);
        return report == null ? null : new ReportDto(report);
    }

    public async Task<IEnumerable<ReportDto>> GetReportsByCityAsync(string city)
    {
        var reports = await _reportRepository.GetByCityAsync(city);
        return reports.Select(r => new ReportDto(r));
    }

    public async Task<IEnumerable<ReportDto>> GetReportsByUserAsync(int userId)
    {
        var reports = await _reportRepository.GetByUserAsync(userId);
        return reports.Select(r => new ReportDto(r));
    }

    public async Task<IEnumerable<byte[]>> GetReportPhotosAsync(int reportId)
    {
        var report = await _reportRepository.GetByIdAsync(reportId) ?? throw new Exception("Report not found.");
        return report.Photos.Select(p => p.ImageData);
    }

    public async Task<ReportDto> UpdateReportAsync(int reportId, UpdateReportDto updateReportDto)
    {
        var report = await _reportRepository.GetByIdAsync(reportId) ?? throw new Exception("Report not found.");

        if (updateReportDto.Title != null)
        {
            report.Title = updateReportDto.Title;
        }

        if (updateReportDto.Description != null)
        {
            report.Description = updateReportDto.Description;
        }

        if (updateReportDto.Latitude != null)
        {
            report.Latitude = updateReportDto.Latitude;
        }

        if (updateReportDto.Longitude != null)
        {
            report.Longitude = updateReportDto.Longitude;
        }

        if (updateReportDto.Status != null)
        {
            report.Status = updateReportDto.Status.Value;
        }

        await _reportRepository.UpdateAsync(report);

        return new ReportDto(report);
    }
}
