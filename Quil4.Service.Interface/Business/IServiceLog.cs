using System;
using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface IServiceLog
    {
        bool CanWriteToLog(out Exception exception);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogException(Exception exception, LogLevel logLevel);
        IEnumerable<IServiceLogItem> GetAllLogEntries();
    }
}