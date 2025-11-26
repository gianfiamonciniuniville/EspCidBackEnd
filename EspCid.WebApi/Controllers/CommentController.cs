using System;
using System.Linq;
using System.Threading.Tasks;
using EspCid.Application.DTO;
using EspCid.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace EspCid.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    [EndpointSummary("Cria um novo comentário")]
    public async Task<IActionResult> CreateComment([FromForm] CreateCommentDto createCommentDto)
    {
        try
        {
            var comment = await _commentService.CreateCommentAsync(createCommentDto);
            return Ok(comment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [EndpointSummary("Obtém um comentário pelo ID")]
    public async Task<IActionResult> GetComment(int id)
    {
        try
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("report/{reportId}")]
    [EndpointSummary("Obtém todos os comentários de um relato")]
    public async Task<IActionResult> GetCommentsByReportId(int reportId)
    {
        try
        {
            var comments = await _commentService.GetCommentsByReportIdAsync(reportId);
            return Ok(comments);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    [EndpointSummary("Atualiza um comentário existente")]
    public async Task<IActionResult> UpdateComment(int id, UpdateCommentDto updateCommentDto)
    {
        try
        {
            var comment = await _commentService.UpdateCommentAsync(id, updateCommentDto);
            return Ok(comment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [EndpointSummary("Deleta um comentário")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        try
        {
            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}