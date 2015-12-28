using System;

namespace Quilt4.Service.Interface.Business
{
    public interface IServiceLogItem
    {
        string Message { get; set; }
        DateTime LogTime { get; set; }
        string Level { get; set; }
    }
}