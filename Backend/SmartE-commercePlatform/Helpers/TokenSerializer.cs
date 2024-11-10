using Application.DTOs.User;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace SmartE_commercePlatform.Helpers
{
    public class TokenSerializer
    {
        private readonly SymmetricSecurityKey key = new (SecretProvider.Instance.Secret);
        private readonly JwtSecurityTokenHandler handler = new();

        public string CreateToken(UserDTO user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new ("id", user.Id.ToString()),
                    new ("username", user.Username),
                    new ("email", user.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(tokenDescriptor);

            var jwtToken = handler.WriteToken(token);

            return jwtToken;
        }

    }
}
