using System;
using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface ITargetAgentBusiness
    {
        IEnumerable<ITargetAgent> GetTargetAgents(Guid projectKey);
    }
}