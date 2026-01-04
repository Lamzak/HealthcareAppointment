using HealthcareAppointment.Application.Interfaces;
using HealthcareAppointment.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthcareAppointment.Infrastructure.Services
{
    public class JwtProvider(IConfiguration configuration) : IJwtProvider
    {
        public string Generate(Patient patient)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, patient.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, patient.Email),
            new Claim("FullName", patient.FullName)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
