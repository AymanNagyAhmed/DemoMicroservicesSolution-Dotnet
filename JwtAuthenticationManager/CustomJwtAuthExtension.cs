using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace JwtAuthenticationManager
{
    public static class CustomJwtAuthExtension
    {
        public static void AddCustomJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenHandler.JWT_SECURITY_KEY))
                };

            });

        }

        private static string GetJwtSecurityKey()
        {
            // Retrieve the JWT security key from environment variables or another secure source
            var jwtSecurityKey = Environment.GetEnvironmentVariable("JWT_SECURITY_KEY");
            if (string.IsNullOrEmpty(jwtSecurityKey))
            {
                throw new InvalidOperationException("JWT security key is not set in the environment variables.");
            }
            return jwtSecurityKey;
        }
    }
}
