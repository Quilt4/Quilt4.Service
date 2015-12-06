namespace Quilt4.Service.DataTransfer
{
    public class IssueTypeRequest
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string IssueLevel { get; set; }
        public string Type { get; set; }
        public IssueTypeRequest Inner { get; set; }
    }
}