using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Quilt4.Api.Controllers
{
    public class HomeController : Controller
    {
        [Route("[controller]")]
        // GET: /<controller>/
        public IActionResult Index()
        {
            return Content("It's Working!");
        }
    }
}
