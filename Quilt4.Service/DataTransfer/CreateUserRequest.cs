using System.Collections.Generic;

namespace Quilt4.Service.DataTransfer
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}