using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Business
{
    public interface IIssueBusiness
    {
        RegisterIssueResponseEntity RegisterIssue(RegisterIssueRequestEntity toRegisterIssueRequestEntity);
    }
}