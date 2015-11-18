using System;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Repository.MemoryRepository
{
    public class MemoryRespository : IRepository
    {
        public void SaveUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public void SaveLoginSession(LoginSession loginSession)
        {
            throw new NotImplementedException();
        }

        public T GetSetting<T>(string name)
        {
            throw new NotImplementedException();
        }

        public void SetSetting<T>(string name, T value)
        {
            throw new NotImplementedException();
        }

        public int GetNextTicket(string clientToken, string name, string version, string type)
        {
            throw new NotImplementedException();
        }
    }
}