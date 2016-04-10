using System.Linq;
using System.Web.Mvc;
using Quilt4.Service.Areas.Admin.Models;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Areas.Admin.Controllers
{
    [Authorize(Roles = Constants.Administrators)]
    public class UserController : Controller
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        // GET: Admin/User
        public ActionResult Index()
        {
            var users = _userBusiness.GetAllUsers();
            var userModels = users.Select(x => new UserModel { FullName = x.FullName, UserName = x.Username, AvatarUrl = x.AvatarUrl, Email = x.Email, UserKey = x.UserKey }).ToArray();
            return View(userModels);
        }
    }
}