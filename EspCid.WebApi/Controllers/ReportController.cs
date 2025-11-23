using System;
using System.Threading.Tasks;
using EspCid.Application.DTO;
using EspCid.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EspCid.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost]
    [EndpointSummary("Cria um novo relatório")]
    public async Task<IActionResult> CreateReport([FromForm] CreateReportDto createReportDto)
    {
        try
        {
            var report = await _reportService.CreateReportAsync(createReportDto);
            return Ok(report);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [EndpointSummary("Atualiza um relatório existente")]
    public async Task<IActionResult> UpdateReport(int id, UpdateReportDto updateReportDto)
    {
        try
        {
            var report = await _reportService.UpdateReportAsync(id, updateReportDto);
            return Ok(report);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [EndpointSummary("Obtém um relatório pelo ID")]
    public async Task<IActionResult> GetReport(int id)
    {
        try
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [EndpointSummary("Obtém todos os relatórios")]
    public async Task<IActionResult> GetAllReports()
    {
        try
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("city/{city}")]
    [EndpointSummary("Obtém relatórios por cidade")]
    public async Task<IActionResult> GetReportsByCity(string city)
    {
        try
        {
            var reports = await _reportService.GetReportsByCityAsync(city);
            return Ok(reports);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("user/{userId}")]
    [EndpointSummary("Obtém relatórios por usuário")]
    public async Task<IActionResult> GetReportsByUser(int userId)
    {
        try
        {
            var reports = await _reportService.GetReportsByUserAsync(userId);
            return Ok(reports);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}/photos")]
    [EndpointSummary("Obtém as fotos de um relatório")]
    public async Task<IActionResult> GetReportPhotos(int id)
    {
        try
        {
            var photos = await _reportService.GetReportPhotosAsync(id);
            var fileContentResults = photos.Select(photo => File(photo, "image/jpeg")).ToList();
            return Ok(fileContentResults);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
