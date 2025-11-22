namespace EspCid.Domain.Entities;

public class Photo : Entity
{
    public byte[] ImageData { get; set; } = [];
    public int ReportId { get; set; }
    public Report Report { get; set; } = null!;
}
