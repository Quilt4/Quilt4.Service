using System;
using System.Collections.Generic;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Repository.SqlRepository
{
    //TODO: Persist to SQL Server
    public class SqlRepository : IRepository
    {
        private static readonly IDictionary<string, string> _settings = new Dictionary<string, string>();
        private static readonly IDictionary<string, User> _users = new Dictionary<string, User>();
        private static readonly IDictionary<string, LoginSession> _loginSession = new Dictionary<string, LoginSession>();

        public void SaveUser(User user)
        {
            _users.Add(user.Username, user);
        }

        public User GetUser(string username)
        {
            if (!_users.ContainsKey(username))
                return null;
            return _users[username];
        }

        public void SaveLoginSession(LoginSession loginSession)
        {
            _loginSession.Add(loginSession.PublicKey, loginSession);
        }

        public T GetSetting<T>(string name)
        {
            if (!_settings.ContainsKey(name))
            {
                return default(T);
            }

            var value = _settings[name];
            var result = (T)Convert.ChangeType(value, typeof(T));
            return result;
        }

        public T GetSetting<T>(string name, T defaultValue)
        {
            if (!_settings.ContainsKey(name))
            {
                SetSetting(name, defaultValue);
            }

            var value = _settings[name];
            var result = (T)Convert.ChangeType(value, typeof(T));
            return result;
        }

        public void SetSetting<T>(string name, T value)
        {
            if (_settings.ContainsKey(name))
                _settings.Remove(name);
            _settings.Add(name, value.ToString());
        }
    }
}