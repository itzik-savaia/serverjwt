using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using JWT.Services;

namespace JWT
{
    public interface IJwtAutenticationManager
    {
        string Authenticate(string username, string password, string userJson);

    }
    public class JwtAutenticationManager : IJwtAutenticationManager
    {
        private readonly string key;

        public JwtAutenticationManager(string key)
        {
            this.key = key;
        }
        public string Authenticate(string username, string password, string userJson)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var toeknKey = Encoding.ASCII.GetBytes(key);
            var tokenDesriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(toeknKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDesriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
