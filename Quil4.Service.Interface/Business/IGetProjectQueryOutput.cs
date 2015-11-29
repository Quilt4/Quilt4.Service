using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface IGetProjectQueryOutput
    {
        IEnumerable<IProject> Projects { get; }
    }
}