namespace Quilt4.Service.SqlRepository.Extensions
{
    public static class SessionExtensions
    {
        public static Entity.Session ToSessionEntity(this Session item)
        {
            if (item == null)
                return null;

            return new Entity.Session
            {
                SessionKey = item.SessionKey,
                ProjectKey = item.Version.Application.Project.ProjectKey,
                ApplicationKey = item.Version.Application.ApplicationKey,
                VersionKey = item.Version.VersionKey,
                CallerIp = item.CallerIp,
                ServerEndTime = item.EndServerTime,
            };
        }
    }
}