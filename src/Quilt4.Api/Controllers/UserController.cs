using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Quilt4.Api.DataTransfer;
using Quilt4.Api.Interfaces;

namespace Quilt4.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost("Create")]
        public void Post([FromBody]CreateUserRequest value)
        {
            _userBusiness.CreateUser(value.Username, value.Email, value.Password);
        }

        [HttpPost("Login")]
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