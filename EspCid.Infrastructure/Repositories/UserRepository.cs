using EspCid.Domain.Entities;
using EspCid.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EspCid.Infrastructure.Repositories;

public class UserRepository(EspCidDbContext context) : IUserRepository
{
    private readonly EspCidDbContext _context = context;

    public async Task<User> CreateAsync(User user)
    {
        user.Created = DateTime.UtcNow;
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    { 
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<User> UpdateAsync(User user)
    {
        user.Updated = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}