namespace Quilt4.Api.Entities
{
    public class Project
    {
        public string Name { get; set; }
    }

    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}