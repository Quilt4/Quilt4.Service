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
                ProjectKey = item.Application.Project.Id,
                ApplicationKey = item.ApplicationKey,
                VersionKey = item.VersionKey,
                CallerIp = item.CallerIp,
                ServerEndTime = item.ServerEndTime,
            };
        }
    }
}