using System.Collections.Generic;
using Quilt4.Api.Entities;

namespace Quilt4.Api.Interfaces
{
    public interface IRepository
    {
        void SaveUser(User user);
        User GetUser(string username);
        void SaveLoginSession(LoginSession loginSession);
        T GetSetting<T>(string name);
        void SetSetting<T>(string name, T value);
        IEnumerable<Project> GetProjects();
        void SaveProject(Project project);
    }
}