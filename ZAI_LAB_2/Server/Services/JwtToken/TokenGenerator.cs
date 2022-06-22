using ZAI_LAB_2.Shared;
//using ZAI_LAB_2.Shared.DTO;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ZAI_LAB_2.Server.Services.JwtToken
{
    public class TokenGenerator
    {
        private readonly KonfiguracjaToken konfiguracjaToken;

        public TokenGenerator(KonfiguracjaToken konfiguracjaToken)
        {
            this.konfiguracjaToken = konfiguracjaToken;
        }

        public string GenerujToken(User login)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(konfiguracjaToken.TokenSecret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name,login.Login));
            foreach (var item in login.Usery)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.Role.RoleName));
            }
            
            JwtSecurityToken token = new JwtSecurityToken(
                konfiguracjaToken.Issure,
                konfiguracjaToken.Audience,
                claims,
                System.DateTime.UtcNow,
                System.DateTime.UtcNow.AddMinutes(konfiguracjaToken.TokenMinutes),
                credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
