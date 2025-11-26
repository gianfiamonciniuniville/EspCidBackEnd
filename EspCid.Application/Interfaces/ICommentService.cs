using System.Collections.Generic;
using System.Threading.Tasks;
using EspCid.Application.DTO;

namespace EspCid.Application.Interfaces;

public interface ICommentService
{
    Task<CommentDto> CreateCommentAsync(CreateCommentDto createCommentDto);
    Task<CommentDto?> GetCommentByIdAsync(int commentId);
    Task<IEnumerable<CommentDto>> GetCommentsByReportIdAsync(int reportId);
    Task<CommentDto> UpdateCommentAsync(int commentId, UpdateCommentDto updateCommentDto);
    Task DeleteCommentAsync(int commentId);
}