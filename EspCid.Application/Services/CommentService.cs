using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EspCid.Application.DTO;
using EspCid.Application.Interfaces;
using EspCid.Domain.Entities;
using EspCid.Domain.Interfaces;

namespace EspCid.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IReportRepository _reportRepository; // Needed to validate ReportId

    public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IReportRepository reportRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _reportRepository = reportRepository;
    }

    public async Task<CommentDto> CreateCommentAsync(CreateCommentDto createCommentDto)
    {
        var user = await _userRepository.GetByIdAsync(createCommentDto.UserId)
                   ?? throw new Exception("User not found.");
        
        var report = await _reportRepository.GetByIdAsync(createCommentDto.ReportId)
                     ?? throw new Exception("Report not found.");

        var comment = new Comment
        {
            Content = createCommentDto.Content,
            ReportId = createCommentDto.ReportId,
            UserId = createCommentDto.UserId,
        };

        foreach (var photo in createCommentDto.Photos)
        {
            using var memoryStream = new MemoryStream();
            await photo.CopyToAsync(memoryStream);
            var imageDataString = Convert.ToBase64String(memoryStream.ToArray());
            var imageDataPrefix = "data:image/jpeg;base64,";
            if (!imageDataString.StartsWith(imageDataPrefix))
                imageDataString = imageDataPrefix + imageDataString;
            comment.Photos.Add(new CommentPhoto { ImageData = System.Text.Encoding.UTF8.GetBytes(imageDataString) });
        }

        await _commentRepository.CreateAsync(comment);

        // Manually assign user and report for DTO conversion since they might not be eagerly loaded
        comment.User = user;
        comment.Report = report; 

        return new CommentDto(comment);
    }

    public async Task<CommentDto?> GetCommentByIdAsync(int commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        return comment == null ? null : new CommentDto(comment);
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByReportIdAsync(int reportId)
    {
        var comments = await _commentRepository.GetByReportIdAsync(reportId);
        return comments.Select(c => new CommentDto(c));
    }

    public async Task<CommentDto> UpdateCommentAsync(int commentId, UpdateCommentDto updateCommentDto)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId)
                      ?? throw new Exception("Comment not found.");

        if (updateCommentDto.Content != null)
        {
            comment.Content = updateCommentDto.Content;
        }

        await _commentRepository.UpdateAsync(comment);
        return new CommentDto(comment);
    }

    public async Task DeleteCommentAsync(int commentId)
    {
        await _commentRepository.DeleteAsync(commentId);
    }
}