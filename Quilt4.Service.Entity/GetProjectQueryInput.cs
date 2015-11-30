using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Entity
{
    public class GetProjectQueryInput : IGetProjectQueryInput
    {
        public GetProjectQueryInput(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}