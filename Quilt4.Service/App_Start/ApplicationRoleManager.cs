using Microsoft.AspNet.Identity;
using Quilt4.Service.Authentication;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.SqlRepository;

namespace Quilt4.Service
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        private ApplicationRoleManager(IUserRepository repository, ISourceRepository sourceRepository)
            : base(new CustomRoleStore<ApplicationRole>(repository, sourceRepository))
        {
        }

        public static ApplicationRoleManager Create(IUserRepository repository, ISourceRepository sourceRepository)
        {
            return new ApplicationRoleManager(repository, sourceRepository);
        }
    }
}