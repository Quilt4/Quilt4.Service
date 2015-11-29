using System;
using System.Web.Http;
using Quilt4.Service.Business.Handlers.Commands;
using Quilt4.Service.Controllers.Project.DataTransfer;

namespace Quilt4.Service.Controllers.Project
{
    public class UpdateProjectController : ApiController
    {
        private readonly UpdateProjectCommandHandler _handler;

        public UpdateProjectController(UpdateProjectCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [Authorize]
        [Route("api/Project/Update")]
        public void UpdateProject(UpdateProjectRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
            if (request.ProjectKey == Guid.Empty) throw new ArgumentException("Key cannot be empty Guid.");
            if (string.IsNullOrEmpty(request.Name)) throw new ArgumentException("No name provided.");
            if (string.IsNullOrEmpty(request.DashboardColor)) throw new ArgumentException("No dashboard color provided.");

            _handler.StartHandle(new UpdateProjectCommandInput(User.Identity.Name, request.ProjectKey, request.Name, request.DashboardColor));
        }
    }
}