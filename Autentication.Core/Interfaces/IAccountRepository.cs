using Autentication.Core.DTO;
using Autentication.Core.Models;

namespace Autentication.Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> GetUserByNameAsync(string username);
        Task UpdateLoginAttempt(User user);
        Task AddUserAsync(User user);
    }
}