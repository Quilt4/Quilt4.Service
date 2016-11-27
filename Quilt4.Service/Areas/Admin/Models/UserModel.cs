using System.Collections.Generic;

namespace Quilt4.Service.Areas.Admin.Models
{
    public class UserModel
    {
        public string UserKey { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public string[] Roles { get; set; }
    }
}