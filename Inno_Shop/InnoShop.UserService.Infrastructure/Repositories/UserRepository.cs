using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoShop.UserService.Domain.Entities;
using InnoShop.UserService.Application.Interfaces;
using InnoShop.UserService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InnoShop.UserService.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await context.Users.FindAsync(id);
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email==email);
        }
        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await context.Users.ToListAsync();
        }
        public async Task AddAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }
        public async Task<User?> GetByResetTokenAsync(string Token)
        {
            return await context.Users.FirstOrDefaultAsync(p=>p.PasswordResetToken==Token);
        }
        public async Task<User?> GetByEmailConfirmationTokenAsync(string Token)
        {
            return await context.Users.FirstOrDefaultAsync(e=>e.EmailConfirmationToken==Token);
        }
    }
}
