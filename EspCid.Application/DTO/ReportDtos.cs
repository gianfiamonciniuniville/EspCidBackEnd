using System;
using System.Collections.Generic;
using EspCid.Domain.Entities;
using EspCid.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace EspCid.Application.DTO;

public class ReportDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Localization { get; set; } = string.Empty;
    public ReportStatus Status { get; set; }
    public UserShortDto User { get; set; } = null!;
    public int PhotosCount { get; set; }
    public DateTime Created { get; set; }

    public ReportDto(Report report)
    {
        Id = report.Id;
        Title = report.Title;
        Description = report.Description;
        Localization = report.Localization;
        Status = report.Status;
        User = new UserShortDto
        {
            Id = report.User.Id,
            FirstName = report.User.FirstName
        };
        PhotosCount = report.Photos.Count;
        Created = report.Created;
    }
}

public class CreateReportDto
{
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Localization { get; set; } = string.Empty;
    public List<IFormFile> Photos { get; set; } = new List<IFormFile>();
}

public class UpdateReportDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ReportStatus? Status { get; set; }
}
