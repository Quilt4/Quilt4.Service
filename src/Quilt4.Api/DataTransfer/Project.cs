namespace Quilt4.Api.DataTransfer
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Versions { get; set; }
        public int Sessions { get; set; }
        public int Exceptions { get; set; }
        public int Errors { get; set; }
        public string DashboardColor { get; set; }
    }
}