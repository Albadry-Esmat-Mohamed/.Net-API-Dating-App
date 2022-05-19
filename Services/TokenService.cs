using Dating_App_API.Interfaces;
using Dating_App_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dating_App_API.Services
{
    public class TokenService : ITokenService
    {
        private SymmetricSecurityKey key;
        public TokenService(IConfiguration config)
        {
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(User user)
        {

            //1 creating claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };

            //2 creating Credentials
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            //3 creating Token Discriptor
            var tokenDiscriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds,
                Expires = DateTime.Now.AddDays(7),

            };

            //4 Creating Token Handller
            var tokenHandler = new JwtSecurityTokenHandler();

            //5 Crating Token
            var token = tokenHandler.CreateToken(tokenDiscriptor);

            //6 Write Token To Return 
            return tokenHandler.WriteToken(token);
            
        }

    }
}
