using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Quilt4.Service.Authentication;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.Models;

namespace Quilt4.Service
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly IUserRepository _repository;
        private readonly ISourceRepository _sourceRepository;

        private ApplicationUserManager(IUserRepository repository, ISourceRepository sourceRepository)
            : base(new CustomUserStore<ApplicationUser>(repository, sourceRepository))
        {
            _repository = repository;
            _sourceRepository = sourceRepository;
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

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            _sourceRepository.RegisterCommand();

            var response = await base.CreateAsync(user);
            if (response.Succeeded)
                AssureAdministrator(user);
            return response;
        }

        public override async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            var response = await base.CreateAsync(user, password);
            return response;
        }

        private void AssureAdministrator(ApplicationUser user)
        {
            var cntStr = System.Configuration.ConfigurationManager.AppSettings["MakeFirstUsersAdmin"];
            int cnt;
            if (!int.TryParse(cntStr, out cnt))
                cnt = 2;

            if (_repository.GetUsers().Count() <= cnt)
            {
                if (_repository.GetRole(Constants.Administrators) == null)
                    _repository.CreateRole(new Role(Constants.Administrators));
                _repository.AddUserToRole(user.UserName, Constants.Administrators);
            }
        }

        public static ApplicationUserManager Create(IUserRepository repository, ISourceRepository sourceRepository)
        {
            return new ApplicationUserManager(repository, sourceRepository);
        }

        public void AddExtraInfo(ApplicationUser user, RegisterViewModel model, string callerIp)
        {
            _repository.AddUserExtraInfo(user.UserName, model.FullName);
        }
    }
}