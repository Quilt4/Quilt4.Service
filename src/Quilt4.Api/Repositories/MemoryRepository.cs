using System.Collections.Generic;
using Quilt4.Api.Entities;
using Quilt4.Api.Interfaces;

namespace Quilt4.Api.Repositories
{
    public class MemoryRepository : IRepository
    {
        private static readonly IDictionary<string, User> _users = new Dictionary<string, User>();
        private static readonly IDictionary<string, LoginSession> _loginSession = new Dictionary<string, LoginSession>();

        public void SaveUser(User user)
        {
            _users.Add(user.Username, user);
        }

        public void SaveLoginSession(LoginSession loginSession)
        {
            _loginSession.Add(loginSession.SessionKey, loginSession);
        }
    }
}