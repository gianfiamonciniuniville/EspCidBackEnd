namespace EspCid.Domain.Entities;

public class CommentPhoto : Entity
{
    public byte[] ImageData { get; set; } = [];
    public int CommentId { get; set; }
    public Comment Comment { get; set; } = null!;
}