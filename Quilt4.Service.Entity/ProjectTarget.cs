namespace Quilt4.Service.Entity
{
    public class ProjectTarget
    {
        public ProjectTarget(string targetType, string connection, bool enabled)
        {
            TargetType = targetType;
            Connection = connection;
            Enabled = enabled;
        }

        public string TargetType { get; }
        public string Connection { get; }
        public bool Enabled { get; }
    }
}