using System;
using System.Collections.Generic;

namespace EspCid.Domain.Entities;

public class Comment : Entity
{
    public string Content { get; set; } = string.Empty;
    public int ReportId { get; set; }
    public Report Report { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<CommentPhoto> Photos { get; set; } = new List<CommentPhoto>();
}