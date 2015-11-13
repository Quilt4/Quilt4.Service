using System.Web.Http;
using Quil4.Service.Interface.Business;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("api/user/create")]
        public void Post([FromBody]CreateUserRequest value)
        {
            _userBusiness.CreateUser(value.Username, value.Email, value.Password);
        }

        [HttpPost]
        [Route("api/user/login")]
        public LoginResponse Post([FromBody]LoginRequest value)
        {
            var login = _userBusiness.Login(value.Username, value.Password);
            return new LoginResponse
            {
                SessionKey = login.SessionKey,
            };
        }
    }
}