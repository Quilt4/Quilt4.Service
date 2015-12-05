using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Authentication
{
    public class CustomUserSore<T> 
        : IUserPasswordStore<T>, IUserStore<T> where T : ApplicationUser
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
            await Task.Run(() => _repository.SaveUser(new User(Guid.NewGuid().ToString(), user.UserName, user.Email, user.PasswordHash)));
        }

        public async Task UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindByNameAsync(string userName)
        {
            var user = await Task.Run(() => _repository.GetUser(userName));
            if (user == null)
            {
                return null;
            }

            var applicationUser = new ApplicationUser { Id = user.UserKey, UserName = user.Username, Email = user.Email };
            var response = (T)applicationUser;
            return response;
        }

        public async Task SetPasswordHashAsync(T user, string passwordHash)
        {
            await Task.Run(() => user.PasswordHash = passwordHash);
        }

        public async Task<string> GetPasswordHashAsync(T user)
        {
            var response = await Task.Run(() => _repository.GetUser(user.UserName));
            return response.PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(T user)
        {
            throw new NotImplementedException();
        }
    }
}