using System;
using System.Collections.Generic;
using EspCid.Domain.Enums;

namespace EspCid.Domain.Entities;

public class Report : Entity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Localization { get; set; } = string.Empty; // For now, we'll use a string. We can improve this later.
    public ReportStatus Status { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
}
