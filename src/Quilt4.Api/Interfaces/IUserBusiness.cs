using Quilt4.Api.Entities;

namespace Quilt4.Api.Interfaces
{
    public interface IUserBusiness
    {
        void CreateUser(string username, string email, string password);
        LoginSession Login(string username, string password);
    }
}