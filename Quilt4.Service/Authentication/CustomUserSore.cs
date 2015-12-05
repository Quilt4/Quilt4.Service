using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public async Task SetPasswordHashAsync(T user, string passwordHash)
        {
            //TODO: Store password here
            throw new NotImplementedException();
        }

        public async Task<string> GetPasswordHashAsync(T user)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasPasswordAsync(T user)
        {
            throw new NotImplementedException();
        }
    }
}