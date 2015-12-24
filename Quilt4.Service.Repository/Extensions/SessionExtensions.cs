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
                SessionKey = item.Id,
                ProjectKey = item.Application.Project.Id,
                //ApplicationName = item.Application.Name,
                ApplicationKey = item.ApplicationId,
                //Version = item.Version.Version1,
                VersionKey = item.VersionId,
                CallerIp = item.CallerIp,
            };
        }
    }
}