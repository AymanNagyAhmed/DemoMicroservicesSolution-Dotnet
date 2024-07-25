using JwtAuthenticationManager.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "CT7hr2XkrjZx4N0YSRq4p56gFL7JHwqT";
        private const int JWT_EXPIRY_IN_MINUTES = 60;
        private readonly List<UserAccount> _userAccountList;

        public JwtTokenHandler()
        {
            _userAccountList = new List<UserAccount>
                {
                    new UserAccount { UserName = "admin", Email = "admin@gmail.com", Password = "admin", Role = "Admin"},
                    new UserAccount { UserName = "user", Email = "user@gmail.com", Password = "user", Role = "User"},
                    new UserAccount { UserName = "user2", Email = "test2@gmail.com", Password = "12345", Role = "User"},
                    new UserAccount { UserName = "test3", Email = "test3@gmail.com", Password = "12345", Role = "User"},
                };
        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authRequest)
        {
            if (string.IsNullOrEmpty(authRequest.UserName) || string.IsNullOrEmpty(authRequest.Password))
            {
                return null;
            }
            /* Validation */
            var userAccount = _userAccountList.FirstOrDefault(u => u.UserName == authRequest.UserName && u.Password == authRequest.Password);
            if (userAccount == null)
            {
                return null;
            }

            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(JWT_EXPIRY_IN_MINUTES);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
                {
                     new Claim(JwtRegisteredClaimNames.Name, authRequest.UserName),
                     new Claim("Role", userAccount.Role)
                });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new AuthenticationResponse
            {
                UserName = userAccount.UserName,
                Email = userAccount.Email,
                Role = userAccount.Role,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
                JwtToken = token,
            };



        }

    }
}
