using System.Web.Http;
using System.Web.Mvc;

namespace Quilt4.Service.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{action}/{id}",
                new { controller = "Admin", action = "Index", id = UrlParameter.Optional });

            //AdminPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}