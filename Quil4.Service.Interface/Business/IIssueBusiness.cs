namespace Quilt4.Service.Interface.Business
{
    public interface IIssueBusiness
    {
        IRegisterIssueResponseEntity RegisterIssue(IRegisterIssueRequestEntity toRegisterIssueRequestEntity);
    }
}