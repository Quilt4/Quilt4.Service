using Microsoft.AspNet.Identity;

namespace Quilt4.Service.Authentication
{
    public class ApplicationRole : IRole
    {
        public string Id { get; }
        public string Name { get; set; }
    }
}