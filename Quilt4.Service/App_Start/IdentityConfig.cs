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
            : base(new CustomUserStore<ApplicationUser>(repository))
        {
            PasswordHasher = new OldSystemPasswordHasher();
        }

        public override IQueryable<ApplicationUser> Users => ((CustomUserStore<ApplicationUser>)Store).GetAll().AsQueryable();

        protected override Task<bool> VerifyPasswordAsync(IUserPasswordStore<ApplicationUser, string> store, ApplicationUser user, string password)
        {
            return base.VerifyPasswordAsync(store, user, password);
        }

        protected override Task<IdentityResult> UpdatePassword(IUserPasswordStore<ApplicationUser, string> passwordStore, ApplicationUser user, string newPassword)
        {
            return base.UpdatePassword(passwordStore, user, newPassword);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password, string callerIp)
        {
            //TODO: Set the maximum calls from the same origin within a sertain time interval (Log violations)

            return base.CreateAsync(user, password);
        }

        public static ApplicationUserManager Create(IRepository repository)
        {
            return new ApplicationUserManager(repository);
        }        
    }
}