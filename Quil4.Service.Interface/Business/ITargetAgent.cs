using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface ITargetAgent
    {
        Task<bool> RegisterIssueAsync(Guid projectKey, string eventName, DateTime serverTime, Dictionary<string, object> tags, Dictionary<string, object> fields);
    }
}