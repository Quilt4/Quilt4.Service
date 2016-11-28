using System.Collections.Generic;
using System.Threading.Tasks;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface ITargetAgent
    {
        Task RegisterIssueAsync(RegisterIssueRequestEntity request, RegisterIssueResponseEntity response, Dictionary<string, object> metadata);
    }
}