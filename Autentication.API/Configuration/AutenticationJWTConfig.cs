using System.Text;
using Autentication.Core.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Autentication.API.Configuration
{
    public static class AutenticationJWTConfig
    {
        public static IServiceCollection AddAutenticationJWTConfig(
            this IServiceCollection services, 
            IConfiguration configuration
        )
        {
            var jwtSection = configuration.GetSection("jwt");

            var jwtOptions = new JwtOptions();

            jwtSection.Bind(jwtOptions);

            services.Configure<JwtOptions>(jwtSection);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }

        public static IApplicationBuilder UseAutenticationJWT(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}