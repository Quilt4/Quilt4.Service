using Microsoft.AspNet.Identity;
using Quilt4.Service.Authentication;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {        
        public ApplicationRoleManager(IRepository repository)
            : base(new CustomRoleStore<ApplicationRole>(repository))
        {
        }

        public static ApplicationRoleManager Create(IRepository repository)
        {
            return new ApplicationRoleManager(repository);
        }
    }
}