using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EspCid.Domain.Entities;
using EspCid.Domain.Interfaces;
using EspCid.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EspCid.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly EspCidDbContext _context;

    public CommentRepository(EspCidDbContext context)
    {
        _context = context;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        comment.Created = DateTime.UtcNow;
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments
            .Include(c => c.User)
            .Include(c => c.Photos)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Comment>> GetByReportIdAsync(int reportId)
    {
        return await _context.Comments
            .Include(c => c.User)
            .Include(c => c.Photos)
            .Where(c => c.ReportId == reportId)
            .OrderByDescending(c => c.Created)
            .ToListAsync();
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        comment.Updated = DateTime.UtcNow;
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment != null)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}