using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IRepository repository) 
            : base(new CustomUserSore<ApplicationUser>(repository))
        {
            //We can retrieve Old System Hash Password and can encypt or decrypt old password using custom approach. 
            //When we want to reuse old system password as it would be difficult for all users to initiate pwd change as per Idnetity Core hashing. 
            this.PasswordHasher = new OldSystemPasswordHasher();
        }

        public override Task<ApplicationUser> FindAsync(string userName, string password)
        {
            Task<ApplicationUser> taskInvoke = Task<ApplicationUser>.Factory.StartNew(() =>
            {
                //First Verify Password... 
                PasswordVerificationResult result = this.PasswordHasher.VerifyHashedPassword(userName, password);
                if (result == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    //Return User Profile Object... 
                    //So this data object will come from Database we can write custom ADO.net to retrieve details/ 
                    ApplicationUser applicationUser = new ApplicationUser();
                    applicationUser.UserName = "san";
                    applicationUser.UserName = "san@san.com";
                    return applicationUser;
                }
                return null;
            });
            return taskInvoke;
        }

        public static ApplicationUserManager Create(IRepository repository)
        {
            return new ApplicationUserManager(repository);
        }
    }
}
