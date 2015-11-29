using System;
using System.Web.Http;
using Quilt4.Service.Business.Handlers.Commands;

namespace Quilt4.Service.Controllers.Project
{
    public class CreateProjectController : ApiController
    {
        private readonly CreateProjectCommandHandler _handler;

        public CreateProjectController(CreateProjectCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [Authorize]
        [Route("api/Project/Create")]
        public void CreateProject(CreateProjectRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
            if (request.Key == Guid.Empty) throw new ArgumentException("Key cannot be empty Guid.");
            if (string.IsNullOrEmpty(request.Name)) throw new ArgumentException("No name provided.");

            _handler.StartHandle(new CreateProjectCommandInput(User.Identity.Name, request.Key, request.Name, request.DashboardColor));
        }
    }
}