using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Autentication.Core.DTO;
using Autentication.Core.Interfaces;
using Autentication.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Autentication.Core.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtOptions _options;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly byte[] _securityKey;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _securityKey = Encoding.UTF8.GetBytes(_options.SecretKey);
        }

        public JsonWebToken GenerateToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString())
                }),

                Expires = DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_securityKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return new JsonWebToken
            {
                AccessToken = _jwtSecurityTokenHandler.WriteToken(token),
                Expires = _options.ExpiryMinutes
            };
        }
    }
}