using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Repository
{
    public interface IRepository
    {
        void SaveUser(User user);
        User GetUser(string username);
        void SaveLoginSession(LoginSession loginSession);
        T GetSetting<T>(string name);
        void SetSetting<T>(string name, T value);
        int GetNextTicket(string clientToken, string name, string version, string type);
    }
}