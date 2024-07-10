using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inventory.Application.DTOs;
using Inventory.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Inventory.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthenticationResultDto> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthenticationResultDto
                {
                    Errors = new[] { "User DONT exist." },
                    Success = false
                };
            }
            var userHasValidPass = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidPass)
            {
                return new AuthenticationResultDto
                {
                    Success = false,
                    Errors = new[] { "User/pass combination is wrong." }
                };
            }

            return GenerateAuthenticationResultForUser(user);
        }

        public async Task<AuthenticationResultDto> UpdateUserAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                return new AuthenticationResultDto
                {
                    Errors = new[] { "User does not exist." },
                    Success = false
                };
            }
            var udpUser = new IdentityUser
            {
                UserName = email,
                PasswordHash = password
            };
            await _userManager.RemovePasswordAsync(existingUser);
            var updateUser = await _userManager.AddPasswordAsync(existingUser, password);
            if (!updateUser.Succeeded)
            {
                return new AuthenticationResultDto
                {
                    Errors = updateUser.Errors.Select(x => x.Description),
                    Success = false
                };
            }

            return GenerateAuthenticationResultForUser(existingUser);
        }

        public async Task<AuthenticationResultDto> RegisterAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return new AuthenticationResultDto
                {
                    Errors = new[] { "User already exist." },
                    Success = false
                };
            }
            var newUser = new IdentityUser
            {
                Email = email,
                UserName = email,
            };
            var createUser = await _userManager.CreateAsync(newUser, password);
            if (!createUser.Succeeded)
            {
                return new AuthenticationResultDto
                {
                    Errors = createUser.Errors.Select(x => x.Description),
                    Success = false
                };
            }

            return GenerateAuthenticationResultForUser(newUser);
        }

        private AuthenticationResultDto GenerateAuthenticationResultForUser(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtOptions:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthenticationResultDto
            {
                Success = true,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
