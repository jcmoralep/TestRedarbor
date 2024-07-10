using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Inventory.Api.DTOs
{
    public class ExceptionDetailsDto : Exception
    {
        public string ErrorMessage { get; set; }
        public HttpStatusCode CodeResponse { get; set; }
        public List<string> Errors { get; set; }
    }
}
