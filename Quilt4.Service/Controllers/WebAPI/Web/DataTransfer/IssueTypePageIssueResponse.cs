using System;
using System.Collections.Generic;

namespace Quilt4.Service.Controllers.WebAPI.Web.DataTransfer
{
    public class IssueTypePageIssueResponse
    {
        public string IssueKey { get; set; }
        public DateTime CreationServerTime { get; set; }
        public string UserName { get; set; }
        public string Enviroment { get; set; }
        public IDictionary<string, string> Data { get; set; }
        public IDictionary<string, string> IssueThreadKeys { get; set; }
    }
}