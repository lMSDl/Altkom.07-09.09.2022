using Microsoft.IdentityModel.Tokens;
using Models;
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

            User user = new User() { UserName = "admin", Role = Roles.Read | Roles.Delete | Roles.Admin };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login)

            };

            claims.AddRange(user.Role.ToString().Split(", ").Select(x => new Claim(ClaimTypes.Role, x)));


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
