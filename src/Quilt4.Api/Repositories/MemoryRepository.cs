using System;
using System.Collections.Generic;
using System.Linq;
using Quilt4.Api.Entities;
using Quilt4.Api.Interfaces;

namespace Quilt4.Api.Repositories
{
    public class MemoryRepository : IRepository
    {
        private static readonly IDictionary<string, User> _users = new Dictionary<string, User>();
        private static readonly IDictionary<string, LoginSession> _loginSession = new Dictionary<string, LoginSession>();
        private static readonly IDictionary<string, Setting> _settings = new Dictionary<string, Setting>();
        private static readonly IDictionary<Guid, Project> _projects = new Dictionary<Guid, Project>();

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
            _loginSession.Add(loginSession.SessionKey, loginSession);
        }

        public T GetSetting<T>(string name)
        {
            if (!_settings.ContainsKey(name))
            {
                return default(T);
            }

            var value = _settings[name].Value;
            var result = (T)Convert.ChangeType(value, typeof(T));
            return result;
        }

        public void SetSetting<T>(string name, T value)
        {
            if (_settings.ContainsKey(name))
                _settings.Remove(name);

            _settings.Add(name, new Setting(name, value.ToString()));
        }

        public IEnumerable<Project> GetProjects()
        {
            if (!_projects.Any())
            {
                var proj1 = new Project(Guid.NewGuid(), "Eplicta2", string.Empty, new[] { new Entities.Version() }, new[] { new Session() }, new[] { new IssueType(new[] { new Issue(), }), }, "red");
                _projects.Add(proj1.ProjectId, proj1);

                var proj2 = new Project(Guid.NewGuid(), "Florida", string.Empty, new[] { new Entities.Version() }, new[] { new Session() }, new[] { new IssueType(new[] { new Issue(), }), }, "blue");
                _projects.Add(proj2.ProjectId, proj2);
            }

            return _projects.Values;
        }

        public void SaveProject(Project project)
        {
            if (_projects.ContainsKey(project.ProjectId))
            {
                _projects.Remove(project.ProjectId);
            }

            _projects.Add(project.ProjectId, project);
        }
    }
}