using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Authentication
{
    public class CustomUserStore<T>
        : IUserPasswordStore<T>, IUserRoleStore<T>, IUserStore<T>, IUserLockoutStore<T, string>, IUserTwoFactorStore<T, string>, IUserLoginStore<T, string>, IUserPhoneNumberStore<T, string> where T : ApplicationUser
    {
        private readonly IRepository _repository;

        public CustomUserStore(IRepository repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
        }

        public async Task CreateAsync(T user)
        {
            await Task.Run(() => _repository.CreateUser(new User(Guid.NewGuid().ToString(), user.UserName, user.Email, user.PasswordHash), DateTime.UtcNow));
        }

        public async Task UpdateAsync(T user)
        {
            await Task.Run(() => _repository.UpdateUser(new User(user.Id, user.UserName, user.Email, user.PasswordHash)));
        }

        public async Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindByIdAsync(string userId)
        {
            var user = await Task.Run(() => _repository.GetUserByUserKey(userId));
            if (user == null)
            {
                return null;
            }

            return (T)ToApplicationUser(user);
        }

        public async Task<T> FindByNameAsync(string userName)
        {
            var user = await Task.Run(() => _repository.GetUserByUserName(userName));
            if (user == null)
            {
                return null;
            }

            return (T)ToApplicationUser(user);
        }

        private static ApplicationUser ToApplicationUser(User user)
        {
            return new ApplicationUser { Id = user.UserKey, UserName = user.Username, Email = user.Email, PasswordHash = user.PasswordHash };
        }

        private static ApplicationUser ToApplicationUser(UserInfo user)
        {
            return new ApplicationUser { Id = user.UserKey, UserName = user.Username, Email = user.Email, PasswordHash = null };
        }

        public async Task SetPasswordHashAsync(T user, string passwordHash)
        {
            await Task.Run(() => user.PasswordHash = passwordHash);
        }

        public async Task<string> GetPasswordHashAsync(T user)
        {
            var response = await Task.Run(() => _repository.GetUserByUserName(user.UserName));
            return response.PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task AddToRoleAsync(T user, string roleName)
        {
            await Task.Run(() => _repository.AddUserToRole(user.UserName, roleName));
        }

        public async Task RemoveFromRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(T user)
        {
            var response = await Task.Run(() => _repository.GetRolesByUser(user.UserName).Select(x => x.RoleName).ToArray());
            return response;
        }

        public async Task<bool> IsInRoleAsync(T user, string roleName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            var response = _repository.GetUsers().Select(ToApplicationUser);
            return response;
        }

        public async Task<DateTimeOffset> GetLockoutEndDateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task SetLockoutEndDateAsync(T user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public async Task<int> IncrementAccessFailedCountAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task ResetAccessFailedCountAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetAccessFailedCountAsync(T user)
        {
            var response = await Task.Run(() => 0);
            return response;
        }

        public async Task<bool> GetLockoutEnabledAsync(T user)
        {
            var response = await Task.Run(() => false);
            return response;
        }

        public async Task SetLockoutEnabledAsync(T user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public async Task SetTwoFactorEnabledAsync(T user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GetTwoFactorEnabledAsync(T user)
        {
            var response = await Task.Run(() => false);
            return response;
        }

        public async Task AddLoginAsync(T user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveLoginAsync(T user, UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(T user)
        {
            var response = await Task.Run(() => new UserLoginInfo[] { });
            return response;
        }

        public async Task<T> FindAsync(UserLoginInfo login)
        {
            throw new NotImplementedException();
        }

        public async Task SetPhoneNumberAsync(T user, string phoneNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPhoneNumberAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task SetPhoneNumberConfirmedAsync(T user, bool confirmed)
        {
            throw new NotImplementedException();
        }
    }
}