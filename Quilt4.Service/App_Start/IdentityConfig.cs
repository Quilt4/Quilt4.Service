using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Quilt4.Service.Models;

namespace Quilt4.Service
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUser : IUser
    {
        public DateTime CreateDate { get; set; }
        public DateTime BirthDate { get; set; }

        public ApplicationUser()
        {
            CreateDate = DateTime.Now;
        }
        public string Id
        {
            get { return "GUID"; }
        }

        public string UserName
        {
            get
            {
                return "san";
            }
            set
            {
                ;
            }
        }

        public string Email { get; set; }
    }

    public class CustomUserSore<T> : IUserStore<T> where T : ApplicationUser
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Task CreateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }

    //public class CustomUserManager : UserManager<ApplicationUser>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager() : base(new CustomUserSore<ApplicationUser>())
        {
            //We can retrieve Old System Hash Password and can encypt or decrypt old password using custom approach. 
            //When we want to reuse old system password as it would be difficult for all users to initiate pwd change as per Idnetity Core hashing. 
            this.PasswordHasher = new OldSystemPasswordHasher();
        }

        public override System.Threading.Tasks.Task<ApplicationUser> FindAsync(string userName, string password)
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
                    return applicationUser;
                }
                return null;
            });
            return taskInvoke;
        }

        public static ApplicationUserManager Create()
        {
            return new ApplicationUserManager();
        }
    }

    /// <summary> 
    /// Use Custom approach to verify password 
    /// </summary> 
    public class OldSystemPasswordHasher : PasswordHasher
    {
        public override string HashPassword(string password)
        {
            return base.HashPassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {

            //Here we will place the code of password hashing that is there in our current solucion.This will take cleartext anad hash 
            //Just for demonstration purpose I always return true.     
            if (true)
            {


                return PasswordVerificationResult.SuccessRehashNeeded;
            }
            else
            {
                return PasswordVerificationResult.Failed;
            }
        }
    }

    //public class ApplicationUserManager : UserManager<ApplicationUser>
    //{
    //    public ApplicationUserManager(IUserStore<ApplicationUser> store)
    //        : base(store)
    //    {
    //    }

    //    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    //    {
    //        var manager = new ApplicationUserManager(new CustomUserSore<ApplicationUser>(context.Get<ApplicationDbContext>()));
    //        // Configure validation logic for usernames
    //        manager.UserValidator = new UserValidator<ApplicationUser>(manager)
    //        {
    //            AllowOnlyAlphanumericUserNames = false,
    //            RequireUniqueEmail = true
    //        };
    //        // Configure validation logic for passwords
    //        manager.PasswordValidator = new PasswordValidator
    //        {
    //            RequiredLength = 6,
    //            RequireNonLetterOrDigit = true,
    //            RequireDigit = true,
    //            RequireLowercase = true,
    //            RequireUppercase = true,
    //        };
    //        var dataProtectionProvider = options.DataProtectionProvider;
    //        if (dataProtectionProvider != null)
    //        {
    //            manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
    //        }
    //        return manager;
    //    }
    //}
}
