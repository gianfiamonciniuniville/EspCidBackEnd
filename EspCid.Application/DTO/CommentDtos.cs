using System;
using System.Collections.Generic;
using EspCid.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace EspCid.Application.DTO;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public UserShortDto User { get; set; } = null!;
    public int PhotosCount { get; set; }

    public CommentDto(Comment comment)
    {
        Id = comment.Id;
        Content = comment.Content;
        Created = comment.Created;
        User = new UserShortDto
        {
            Id = comment.User.Id,
            FirstName = comment.User.FirstName
        };
        PhotosCount = comment.Photos.Count;
    }
}

public class CreateCommentDto
{
    public string Content { get; set; } = string.Empty;
    public int ReportId { get; set; }
    public int UserId { get; set; }
    public List<IFormFile> Photos { get; set; } = new List<IFormFile>();
}

public class UpdateCommentDto
{
    public string? Content { get; set; }
}