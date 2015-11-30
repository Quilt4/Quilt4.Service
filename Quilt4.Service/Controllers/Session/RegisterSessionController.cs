using System;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Quilt4.Service.Business.Handlers.Commands;
using Quilt4.Service.Entity;
using Tharga.Quilt4Net.DataTransfer;

namespace Quilt4.Service.Controllers.Session
{
    public class RegisterSessionController : ApiController
    {
        private readonly RegisterSessionCommandHandler _handler;

        public RegisterSessionController(RegisterSessionCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("api/Session/Register")]
        public void CreateProject(RegisterSessionRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");

            _handler.StartHandle(new RegisterSessionCommandInput());
            //try
            //{
            //    //var data = GetData(request).ToSessionRequestEntity(HttpContext.Current.Request.UserHostAddress);
            //    //_sessionBusiness.RegisterSession(data);


            //}
            //catch (Exception exception)
            //{
            //    //TODO: Log exception
            //    throw;
            //}

            //////if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
            //////if (request.ProjectKey == Guid.Empty) throw new ArgumentException("Key cannot be empty Guid.");
            //////if (string.IsNullOrEmpty(request.Name)) throw new ArgumentException("No name provided.");

            //////_handler.StartHandle(new CreateProjectCommandInput(User.Identity.Name, request.ProjectKey, request.Name, request.DashboardColor));
            ////throw new NotImplementedException();
        }

        //private RegisterSessionRequest GetData(object request)
        //{
        //    var requestString = request.ToString();

        //    RegisterSessionRequest data;
        //    try
        //    {
        //        data = JsonConvert.DeserializeObject<RegisterSessionRequest>(requestString);
        //    }
        //    catch (Exception exception)
        //    {
        //        //TODO: Log exception
        //        throw;
        //    }

        //    return data;
        //}
    }
}