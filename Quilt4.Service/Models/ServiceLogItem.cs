using System;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Models
{
    public class ServiceLogItem : IServiceLogItem
    {
        public string Message { get; set; }
        public DateTime LogTime { get; set; }
        public string Level { get; set; }
    }
}