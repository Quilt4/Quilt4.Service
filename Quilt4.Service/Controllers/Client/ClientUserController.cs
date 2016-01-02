using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.Client
{
    [Authorize]
    public class ClientUserController : ApiController
    {
        private readonly IUserBusiness _userBusiness;

        public ClientUserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [Authorize(Roles = Constants.Administrators)]
        [Route("api/Client/User")]
        public IEnumerable<UserResponse> Get()
        {
            var response = _userBusiness.GetList().Select(x => new UserResponse { UserName = x.Username, EMail = x.Email });
            return response;
        }

        //[Route("api/Client/User/Invite")]
        //public void Invite(InviteRequest inviteRequest)
        //{
        //    _userBusiness.Invite(User.Identity.Name, inviteRequest.ProjectKey, inviteRequest.User);
        //}

        //[Route("api/Client/User/Accept")]
        //public void Accept(InviteAcceptRequest inviteRequest)
        //{
        //    _userBusiness.Accept(User.Identity.Name, inviteRequest.InviteCode);
        //}

        [Route("api/Client/User/UserQuery")]
        public IEnumerable<QueryUserResponse> UserQuery([FromBody]QueryUserRequest queryUserRequest)
        {
            var callerIp = HttpContext.Current.Request.UserHostAddress;
            var response = _userBusiness.SearchUsers(queryUserRequest.SearchString, callerIp).Select(x => new QueryUserResponse { UserName = x.Username, EMail = x.Email });
            return response;
        }
    }

    public class InviteAcceptRequest
    {
        //TODO: Replace this class with the nuget package version
        public string InviteCode { get; set; }
    }
}