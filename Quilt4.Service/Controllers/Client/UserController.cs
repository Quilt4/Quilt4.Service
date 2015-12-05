//using System.Web.Http;
//using Quilt4.Service.DataTransfer;
//using Quilt4.Service.Entity;
//using Quilt4.Service.Interface.Business;

//namespace Quilt4.Service.Controllers
//{
//    public class UserController : ApiController
//    {
//        private readonly IUserBusiness _userBusiness;

//        public UserController(IUserBusiness userBusiness)
//        {
//            _userBusiness = userBusiness;
//        }

//        //[HttpPost]
//        //[Route("api/user/create")]
//        //public void Post([FromBody]CreateUserRequest value)
//        //{
//        //    _userBusiness.CreateUser(value.Username, value.Email, value.Password);
//        //}

//        [HttpPost]
//        [Route("api/user/login")]
//        public LoginResponse Post([FromBody]LoginRequest value)
//        {
//            var loginSession = _userBusiness.Login(value.Username, value.Password, SecurityType.Simple, value.PublicSessionKey);
//            return new LoginResponse
//            {
//                SecurityType = loginSession.SecurityType.ToString(),
//                PublicSessionKey = loginSession.PublicKey,
//                PrivateSessionKey = loginSession.PrivateKey,
//            };
//        }
//    }
//}