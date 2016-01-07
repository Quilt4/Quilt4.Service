using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Models;

namespace Quilt4.Service.Logging
{
    public class EventLogger : IServiceLog
    {
        private const string EventLogName = "Quilt4";
        private const string EventSourceName = "Quilt4.Service";
        private const string EventLogInitialMessage = "Eventlog works as it should.";

        public bool CanWriteToLog(out Exception exception)
        {
            exception = AssureEventLogSource();
            return exception == null;
        }

        public IEnumerable<IServiceLogItem> GetAllLogEntries()
        {
            var eventLogEntries = new List<EventLogEntry>();
            var myLog = new EventLog(EventLogName);
            eventLogEntries.AddRange(myLog.Entries.Cast<EventLogEntry>());

            return eventLogEntries.Where(x => x.Source == EventSourceName).Select(x => new ServiceLogItem { Message = x.Message, LogTime = x.TimeGenerated, Level = x.EntryType.ToString() });
        }

        public void LogInformation(string message)
        {
            WriteToEventLog(message, EventLogEntryType.Information);
        }

        public void LogWarning(string message)
        {
            WriteToEventLog(message, EventLogEntryType.Warning);
        }

        public void LogException(Exception exception, LogLevel logLevel)
        {
            EventLogEntryType eventLogEntryType;
            switch (logLevel)
            {
                case LogLevel.DoNotLog:
                    return;
                case LogLevel.Information:
                    eventLogEntryType = EventLogEntryType.Information;
                    break;
                case LogLevel.Warning:
                    eventLogEntryType = EventLogEntryType.Warning;
                    break;
                default:
                    eventLogEntryType = EventLogEntryType.Error;
                    break;
            }

            var message = GetMessageFromException(exception);
            WriteToEventLog(message, eventLogEntryType);
        }

        private Exception AssureEventLogSource()
        {
            try
            {
                if (!EventLog.SourceExists(EventSourceName))
                {
                    EventLog.CreateEventSource(new EventSourceCreationData(EventSourceName, EventLogName));
                    WriteToEventLog(EventLogInitialMessage, EventLogEntryType.Information);
                }
                return null;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        private void WriteToEventLog(string message, EventLogEntryType eventLogEntryType)
        {
            AssureEventLogSource();

            var myLog = new EventLog(EventLogName);
            myLog.Source = EventSourceName;
            myLog.WriteEntry(message, eventLogEntryType);
        }

        private string GetMessageFromException(Exception exception, bool appendStackTrace = true)
        {
            if (exception == null)
                return null;

            var sb = new StringBuilder();

            sb.AppendFormat("{0} ", exception.Message);
            if (exception.Data.Count > 0)
            {
                sb.Append("[");
                var sb2 = new StringBuilder();
                foreach (DictionaryEntry data in exception.Data)
                {
                    sb2.Append(data.Key + ": " + data.Value + ", ");
                }
                sb.Append(sb2.ToString().TrimEnd(',', ' '));
                sb.Append("]");
            }

            var subMessage = GetMessageFromException(exception.InnerException, false);
            if (!string.IsNullOrEmpty(subMessage))
            {
                sb.AppendFormat(" / {0}", subMessage);
            }

            if (appendStackTrace)
            {
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendFormat("@ {0}", exception.StackTrace);
            }

            return sb.ToString();
        }
    }
}