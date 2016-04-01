namespace Quilt4.Service.Models
{
    public class SystemStatus
    {
        public bool CanWriteToLog { get; set; }
        public bool CanConnectToDatabase { get; set; }
    }
}