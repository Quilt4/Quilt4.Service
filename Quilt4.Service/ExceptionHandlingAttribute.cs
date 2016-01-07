using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        private readonly Action<Exception, LogLevel> _logException;

        public ExceptionHandlingAttribute(Action<Exception, LogLevel> logException)
        {
            _logException = logException;
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            Exception exp;
            try
            {
                var logLevel = GetLogLevel(context.Exception);
                _logException(context.Exception, logLevel);

                var error = ToError(context.Exception);
                var httpResponseMessage = new HttpResponseMessage
                {
                    StatusCode = GetStatusCode(context.Exception),
                    ReasonPhrase = context.Exception.Message,
                    RequestMessage = context.Request,
                    Content = new ObjectContent<Error>(error, new JsonMediaTypeFormatter(), "application/json"),                    
                };
                exp = new HttpResponseException(httpResponseMessage);
            }
            catch (Exception exception)
            {
                var e = new AggregateException(exception, context.Exception);
                _logException(e, LogLevel.SystemError);
                throw;
            }

            throw exp;
        }

        class Error
        {
            public int Code { get; set; }
            public string Type { get; set; }
            public string Message { get; set; }
            public Dictionary<string, string> Data { get; set; }
        }
        
        private static Error ToError(Exception exception)
        {
            var type = exception?.GetType().FullName ?? "Unknown";
            var message = exception?.Message ?? "Unknown error.";
            var dictionary = exception?.Data.Cast<DictionaryEntry>().Where(x => x.Value != null).ToDictionary(item => item.Key.ToString(), item => item.Value.ToString());
            var a = new Error
            {
                Code = 1,
                Type = type,
                Message = message,
                Data = dictionary,                
            };
            return a;
        }

        private static LogLevel GetLogLevel(Exception exception)
        {
            if (exception == null)
                return LogLevel.DoNotLog;

            var exType = exception.GetType().Name;
            switch (exType)
            {
                case "ArgumentException":
                case "ArgumentNullException":
                    return LogLevel.Information;
                default:
                    return LogLevel.Error;
            }
        }

        private static HttpStatusCode GetStatusCode(Exception exception)
        {
            if (exception == null)
                return HttpStatusCode.InternalServerError;

            var exType = exception.GetType().Name;
            switch (exType)
            {
                case "InvalidOperationException":
                    return HttpStatusCode.InternalServerError;
                case "NotImplementedException":
                    return HttpStatusCode.NotImplemented;
                case "AuthenticationException":
                    return HttpStatusCode.Forbidden;
                case "ArgumentException":
                case "ArgumentNullException":
                    return HttpStatusCode.BadRequest;
                //return HttpStatusCode.Created
                //return HttpStatusCode.MethodNotAllowed
                default:
                    //return HttpStatusCode.BadRequest;
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}