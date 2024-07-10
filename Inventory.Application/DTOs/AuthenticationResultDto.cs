namespace Inventory.Application.DTOs
{
    public class AuthenticationResultDto
    {
        public string Token { get; set; }
        public bool Success { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
