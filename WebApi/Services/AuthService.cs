using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApi.Services
{
    public class AuthService
    {
        public static byte[] Key { get; } = Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());

        public string Authenticate(string login, string password)
        {
            if(login != "admin" || password != "nimda")
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login)
            };

            var identity = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor();
            tokenDescriptor.Subject = identity;
            tokenDescriptor.Expires = DateTime.Now.AddSeconds(30);
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
