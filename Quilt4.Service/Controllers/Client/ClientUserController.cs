using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.Client
{
    [Authorize(Roles = Constants.Administrators)]
    public class ClientUserController : ApiController
    {
        private readonly IUserBusiness _userBusiness;

        public ClientUserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        //TODO: Use UserResponse from quilt4net nuget instead
        [Route("api/Client/User")]
        public IEnumerable<UserResponse> Get()
        {
            var response = _userBusiness.GetList().Select(x => new UserResponse { UserName = x.Username, EMail = x.Email });
            return response;
        }
    }

    public class UserResponse
    {
        public string UserName { get; set; }
        public string EMail { get; set; }
    }
}