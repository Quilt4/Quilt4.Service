using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Repository.SqlRepository
{
    public class SqlRepository : IRepository
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

        public int GetNextTicket(string clientToken, string applicationName, string applicationVersion, string type, string level, string message, string stackTrace)
        {
            using (var context = new Quilt4DataContext())
            {
                var project = context.Projects.Single(x => x.ClientToken == clientToken);

                var application = project.Applications.Single(x => x.Name == applicationName);

                var version = application.Versions.Single(x => x.Version1 == applicationVersion);

                var issueType =
                    version.IssueTypes.SingleOrDefault(
                        x => x.Type == type && x.Level == level && x.Message == message && x.StackTrace == stackTrace);

                if (issueType != null)
                    return issueType.Ticket;

                var latestTicket =
                    project.Applications.SelectMany(x => x.Versions).SelectMany(y => y.IssueTypes).Max(z => z.Ticket);

                return latestTicket + 1;
            }
        }
    }
}