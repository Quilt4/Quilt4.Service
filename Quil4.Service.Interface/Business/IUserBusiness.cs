using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IUserBusiness
    {
        //void CreateUser(string username, string email, string password);
        LoginSession Login(string username, string password, SecurityType? securityType = null, string publicKey = null);
    }
}