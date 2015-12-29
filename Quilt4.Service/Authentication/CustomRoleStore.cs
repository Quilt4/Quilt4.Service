using System;
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
}