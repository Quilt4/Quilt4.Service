using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.WebAPI
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [Authorize(Roles = Constants.Administrators)]
        [Route("api/User")]
        public IEnumerable<UserResponse> Get()
        {
            var response = _userBusiness.GetAllUsers().Select(x => new UserResponse { UserName = x.Username, EMail = x.Email });
            return response;
        }
        
        [HttpGet]
        [Route("api/User/{searchString}")]
        public IEnumerable<QueryUserResponse> Get([FromUri]string searchString)
        {
            var callerIp = HttpContext.Current.Request.UserHostAddress;
            var response = _userBusiness.SearchUsers(searchString, callerIp).Select(x => new QueryUserResponse { UserName = x.Username, EMail = x.Email });
            return response;
        }
    }
}