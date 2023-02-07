using Autentication.Core.DTO;
using Autentication.Core.Interfaces;
using Autentication.Core.Models;
using Microsoft.Extensions.Options;

namespace Autentication.Core.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAccountRepository _repository;

        private readonly IJwtHandler _jwtHandler;

        private readonly JwtOptions _options;

        public AccountService(IAccountRepository repository, IJwtHandler jwtHandler, IOptions<JwtOptions> options, INotificator notificator)
        : base(notificator)
        {
            _repository = repository;
            _jwtHandler = jwtHandler;
            _options = options.Value;
        }

        public async Task SignUp(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                Notify($"Username can not be empty.");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                Notify($"Password can not be empty.");
                return;
            }
            if (await _repository.GetUserByNameAsync(username) != null)
            {
                Notify($"Username '{username}' is already in use.");
                return;
            }

            await _repository.AddUserAsync(
                new User
                {
                    Username = username,
                    Password = password,
                    FailedLoginAttempt = 0
                }
            );
        }

        public async Task<JsonWebToken> SignIn(string username, string password)
        {
            var user = await _repository.GetUserByNameAsync(username);

            if(user is null)
            {
                Notify("Invalid credentials.");
                return default;
            }

            if(user.Lockouttime?.AddMinutes(_options.MinutesToWaitForANewLogin) > DateTime.Now)
            {
                Notify($"You have been blocked due to {_options.MaxQtdLoginAttempt} wrong attempt. Try again later.");
                return default;
            }

            if(user.Password == password)
            {
                if(user.FailedLoginAttempt > 0)
                {
                    user.FailedLoginAttempt = 0;

                    await _repository.UpdateLoginAttempt(user);
                }

                var jwt = _jwtHandler.GenerateToken(
                    new User
                    {
                        Password = password,
                        Username = username
                    }
                );
                
                return jwt;
            }
            else
            {
                user.FailedLoginAttempt += 1;

                if(user.FailedLoginAttempt >= _options.MaxQtdLoginAttempt) 
                {
                    user.Lockouttime = DateTime.Now;
                    user.FailedLoginAttempt = 0;
                }

                await _repository.UpdateLoginAttempt(user);

                Notify("Invalid credentials.");
                return default;
            }
        }
    }
}