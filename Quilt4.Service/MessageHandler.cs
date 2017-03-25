using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quilt4.Service.Command;

namespace Quilt4.Service.Command
{
    public class CommandRunner
    {
        private readonly Task _task;

        public CommandRunner(ICommandQueue commandQueue)
        {
            _task = new Task(() =>
            {
                while (true)
                {
                    commandQueue.ItemAdded.WaitOne();

                    HttpRequestMessage item;
                    while (commandQueue.TryTake(out item))
                    {
                        //TODO: Handle the command
                        //TODO: Fire event, returning the result, using signalR.
                    }
                }

                System.Diagnostics.Debug.WriteLine("Exiting command loop.");
            });
        }

        public void Run()
        {
            _task.Start();
        }
    }

    public class CommandQueue : ICommandQueue
    {
        private readonly BlockingCollection<HttpRequestMessage> _queue = new BlockingCollection<HttpRequestMessage>();
        private readonly AutoResetEvent _itemAdded = new AutoResetEvent(false);

        public void Enqueue(HttpRequestMessage request)
        {
            //TODO: Store command in database (So that it can be replayed later)
            _queue.Add(request);
            _itemAdded.Set();
        }

        public bool TryTake(out HttpRequestMessage item)
        {
            return _queue.TryTake(out item);
        }

        public AutoResetEvent ItemAdded => _itemAdded;
    }

    public interface ICommandQueue
    {
        void Enqueue(HttpRequestMessage request);
        bool TryTake(out HttpRequestMessage item);
        AutoResetEvent ItemAdded { get; }
    }
}

namespace Quilt4.Service
{
    //public class MyHttpControllerDispatcher : HttpControllerDispatcher
    //{
    //    public MyHttpControllerDispatcher(HttpConfiguration configuration)
    //        : base(configuration)
    //    {
    //    }

    //    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    //    {
    //        return base.SendAsync(request, cancellationToken);
    //    }
    //}

    //NOTE: Write something on how end, users should handle these kinds of calls in their applications
    public class MessageHandler : DelegatingHandler
    {
        private readonly ICommandQueue _commandQueue;
        //private readonly IServiceBusiness _serviceBusiness;
        //private readonly IServiceLog _serviceLog;

        public MessageHandler(ICommandQueue commandQueue) //IServiceBusiness serviceBusiness, IServiceLog serviceLog)
        {
            _commandQueue = commandQueue;
            //_serviceBusiness = serviceBusiness;
            //_serviceLog = serviceLog;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            switch (request.Method.Method)
            {
                case "POST":
                    _commandQueue.Enqueue(request);
                    return new HttpResponseMessage(HttpStatusCode.Accepted);
                default:
                    throw new ArgumentOutOfRangeException();
            }

//            var time = DateTime.UtcNow;

//            var stopWatch = new Stopwatch();

//            var callKey = Guid.NewGuid();
//            var callerIp = HttpContext.Current.Request.UserHostAddress;
//            var currentUserName = HttpContext.Current.Request.LogonUserIdentity?.Name;
//            var requestType = HttpContext.Current.Request.RequestType;
//            var path = HttpContext.Current.Request.Url.AbsoluteUri;
//            var responseString = string.Empty;

//            try
//            {
//                stopWatch.Start();
//                var response = await base.SendAsync(request, cancellationToken);
//                stopWatch.Stop();

//                var issueKey = await GetError(response);

//                if (response.Content != null)
//                {
//                    responseString = await response.Content.ReadAsStringAsync();
//                }

//                var requestString = GetRequest();
//#pragma warning disable 4014
//                Task.Run(() =>
//#pragma warning restore 4014
//                {
//                    try
//                    {
//                        var callMetadata = GetCallMetadata(requestString);
//                        _serviceBusiness.LogApiCall(callKey, callMetadata.SessionKey, callMetadata.ProjectApiKey, time, stopWatch.Elapsed, callerIp, currentUserName, requestType, path, requestString, responseString, issueKey);
//                    }
//                    catch (Exception exception)
//                    {
//                        _serviceLog.LogException(exception, LogLevel.SystemError);
//                    }
//                });

//                return response;
//            }
//            catch (Exception exception)
//            {
//                //TODO: Log this issue, then log the call
//                responseString = exception.Message;

//                var requestString = GetRequest();

//                var CallMetadata = GetCallMetadata(requestString);
//                _serviceBusiness.LogApiCall(callKey, CallMetadata.SessionKey, CallMetadata.ProjectApiKey, time, stopWatch.Elapsed, callerIp, currentUserName, requestType, path, requestString, responseString, Guid.Empty);

//                throw;
//            }
        }

        private static CallMetadata GetCallMetadata(string requestString)
        {
            var callMetadata = new CallMetadata();
            if (!string.IsNullOrEmpty(requestString))
            {
                try
                {
                    IDictionary<string, JToken> jsonObject = JObject.Parse(requestString);

                    if (jsonObject.ContainsKey("SessionKey"))
                    {
                        callMetadata.SessionKey = jsonObject["SessionKey"].Value<string>();
                    }

                    if (jsonObject.ContainsKey("ProjectApiKey"))
                    {
                        callMetadata.ProjectApiKey = jsonObject["ProjectApiKey"].Value<string>();
                    }
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.Message);
                }
            }

            return callMetadata;
        }

        private static async Task<Guid?> GetError(HttpResponseMessage response)
        {
            Guid? issueKeyU = null;

            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<ExceptionHandlingAttribute.Error>(result);

                if (error.Data != null && error.Data.ContainsKey("IssueKey"))
                {
                    Guid issueKey;
                    if (Guid.TryParse(error.Data["IssueKey"], out issueKey))
                    {
                        issueKeyU = issueKey;
                    }
                }
                else
                    issueKeyU = Guid.Empty;
            }
            return issueKeyU;
        }

        private static string GetRequest()
        {
            string requestString;
            if (HttpContext.Current.Request.InputStream.CanSeek)
            {
                HttpContext.Current.Request.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
            }
            using (var reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                requestString = reader.ReadToEnd();
            }
            return requestString;
        }
    }
}