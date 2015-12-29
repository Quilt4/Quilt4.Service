using System.Collections.Generic;
using System.Linq;
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

        [Route("api/Client/User/Invite")]
        public void Invite(InviteRequest inviteRequest)
        {
            _userBusiness.Invite(User.Identity.Name, inviteRequest.ProjectKey, inviteRequest.User);
        }
    }
}