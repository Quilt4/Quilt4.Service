using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Authentication
{
    public class CustomRoleStore<T>
        : IRoleStore<T> where T : ApplicationRole
    {
        private readonly IRepository _repository;

        public CustomRoleStore(IRepository repository)
        {
            _repository = repository;
        }

        public void Dispose()
        {
        }

        public async Task CreateAsync(T role)
        {
            await Task.Run(() => _repository.CreateRole(new Role(role.Name)));
        }

        public Task UpdateAsync(T role)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T role)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindByNameAsync(string roleName)
        {
            var role = await Task.Run(() => _repository.GetRole(roleName));
            if (role == null) return null;
            var applicationRole = new ApplicationRole { Name = role.RoleName };
            var response = (T)applicationRole;
            return response;
        }
    }

    public class CustomUserSore<T> 
        : IUserPasswordStore<T>, IUserRoleStore<T>, IUserStore<T> where T : ApplicationUser
    {
        private readonly IRepository _repository;

        public CustomUserSore(IRepository repository)
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
            return new ApplicationUser { Id = user.UserKey, UserName = user.Username, Email = user.Email };
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
    }
}