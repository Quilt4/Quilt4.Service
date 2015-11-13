using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Business
{
    public interface IUserBusiness
    {
        void CreateUser(string username, string email, string password);
        LoginSession Login(string username, string password);
    }
}