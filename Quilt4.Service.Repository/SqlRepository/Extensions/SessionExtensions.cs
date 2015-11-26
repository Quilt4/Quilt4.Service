namespace Quilt4.Service.Repository.SqlRepository.Extensions
{
    public static class SessionExtensions
    {
        public static Entity.Session ToSessionEntity(this Session item)
        {
            if (item == null)
                return null;

            return new Entity.Session
            {
                Id = item.Id,
                ApplicationName = item.Application.Name,
                ApplicationId = item.ApplicationId,
                Version = item.Version.Version1,
                VersionId = item.VersionId
            };
        }
    }
}