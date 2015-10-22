using Quilt4.Api.Entities;

namespace Quilt4.Api.Interfaces
{
    public interface IRepository
    {
        void SaveUser(User user);
        void SaveLoginSession(LoginSession loginSession);
    }
}