using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Quilt4.Service.Authentication;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private ApplicationUserManager(IRepository repository) 
            : base(new CustomUserSore<ApplicationUser>(repository))
        {
            PasswordHasher = new OldSystemPasswordHasher();
        }

        public override IQueryable<ApplicationUser> Users => ((CustomUserSore<ApplicationUser>)Store).GetAll().AsQueryable();

        public static ApplicationUserManager Create(IRepository repository)
        {
            return new ApplicationUserManager(repository);
        }
    }
}
