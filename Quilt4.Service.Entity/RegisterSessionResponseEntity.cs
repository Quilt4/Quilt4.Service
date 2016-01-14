using System;

namespace Quilt4.Service.Entity
{
    public class RegisterSessionResponseEntity
    {
        public string SessionKey { get; set; }
        public DateTime ServerStartTime { get; set; }
    }
}