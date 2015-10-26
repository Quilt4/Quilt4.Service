namespace Quilt4.Api.DataTransfer
{
    public class Application
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string Name { get; set; }
        public int Versions { get; set; }
        public int Sessions { get; set; }
        public int IssueTypes { get; set; }
        public int Issues { get; set; }
    }
}