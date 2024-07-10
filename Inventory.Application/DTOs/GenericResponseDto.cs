namespace Inventory.Application.DTOs
{
    public class GenericResponseDto  <T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public GenericResponseDto() { }

        public GenericResponseDto(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}
