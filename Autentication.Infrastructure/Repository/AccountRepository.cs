using Autentication.Core.DTO;
using Autentication.Core.Interfaces;
using Autentication.Core.Models;
using Autentication.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Autentication.Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AutenticationContext _context;

        public AccountRepository(AutenticationContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByNameAsync(string username)
        {
            return await _context.Users
                         .SingleOrDefaultAsync(x => x.Username.ToLower() == username.ToLower());
        }

        public async Task UpdateLoginAttempt(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }
    }
}