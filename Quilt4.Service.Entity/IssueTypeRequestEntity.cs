namespace Quilt4.Service.Entity
{
    public class IssueTypeRequestEntity
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string IssueLevel { get; set; }
        public string Type { get; set; }
        public IssueTypeRequestEntity Inner { get; set; }
    }
}