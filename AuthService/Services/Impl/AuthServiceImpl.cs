using AuthService.Config;

using AuthService.DTOs;
using AuthService.Models;
using AuthService.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Services.Impl
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthServiceImpl(IUserRepository userRepo, IConfiguration configuration)
        {
            _config = configuration;
            _userRepository = userRepo;
        }

        public async Task RegisterUser(RegisterDto dto)
        {
            await RegisterWithRoles(dto, (int)RoleIds.User);
        }

        public async Task RegisterCompanyRepresentative(RegisterDto dto)
        {
            await RegisterWithRoles(dto, (int)RoleIds.CompanyRepresentative);
        }

        public async Task RegisterArbitrator(RegisterDto dto)
        {
            await RegisterWithRoles(dto, (int)RoleIds.Arbitrator);
        }

        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            var user = await _userRepository.GetByEmail(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password");
            }

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token
            };
        }

        private async Task RegisterWithRoles(RegisterDto dto, int roleId)
        {
            if (await _userRepository.EmailExists(dto.Email))
                throw new Exception("Email already registered");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = hashedPassword,
                UserRoles = new List<UserRole>
                {
                    new UserRole { RoleId = roleId }
                }
            };

            await _userRepository.SaveUser(user);


        }

        private string GenerateJwtToken(User user)
        {
            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
