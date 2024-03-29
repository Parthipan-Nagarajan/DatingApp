using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
#pragma warning restore CS8604 // Possible null reference argument.
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(2),
                SigningCredentials = creds
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.CreateToken(tokenDescripter);
            return tokenHandler.WriteToken(tokenString);
        }

        public bool ValidToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}