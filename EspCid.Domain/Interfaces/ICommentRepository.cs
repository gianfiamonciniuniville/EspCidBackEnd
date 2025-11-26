using System.Collections.Generic;
using System.Threading.Tasks;
using EspCid.Domain.Entities;

namespace EspCid.Domain.Interfaces;

public interface ICommentRepository
{
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> GetByIdAsync(int id);
    Task<IEnumerable<Comment>> GetByReportIdAsync(int reportId);
    Task<Comment> UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
}