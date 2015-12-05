using System;
using Microsoft.AspNet.Identity;

namespace Quilt4.Service
{
    public class ApplicationUser : IUser
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
            CreateDate = DateTime.UtcNow;
        }

        public string Id { get; }
        public DateTime CreateDate { get; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}