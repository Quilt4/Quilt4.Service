using System;

namespace Quilt4.Service.Entity
{
    public class RegisterSessionResponseEntity
    {
        public string SessionToken { get; set; }
        public DateTime ServerStartTime { get; set; }
    }
}