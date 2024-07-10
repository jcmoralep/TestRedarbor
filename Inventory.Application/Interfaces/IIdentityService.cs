using Inventory.Application.DTOs;

namespace Inventory.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResultDto> RegisterAsync(string email, string password);
        Task<AuthenticationResultDto> LoginAsync(string email, string password);
        Task<AuthenticationResultDto> UpdateUserAsync(string email, string password);
    }
}
