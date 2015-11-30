using System;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Business;
using Quilt4.Service.Business.Handlers.Commands;
using Quilt4.Service.Business.Handlers.Queries;
using Quilt4.Service.Entity;
using Tharga.Quilt4Net.DataTransfer;

namespace Quilt4.Service.Controllers.Project
{
    public class CreateProjectController : ApiController
    {
        private readonly CreateProjectCommandHandler _command;

        public CreateProjectController(CreateProjectCommandHandler command)
        {
            _command = command;
        }

        [HttpPost]
        [Authorize]
        [Route("api/Project/Create")]
        public CreateProjectResponse CreateProject(CreateProjectRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");
            if (request.ProjectKey == Guid.Empty) throw new ArgumentException("Key cannot be empty Guid.");
            if (string.IsNullOrEmpty(request.Name)) throw new ArgumentException("No name provided.");

            var projectApiKey = RandomUtility.GetRandomString(32);

            _command.StartHandle(new CreateProjectCommandInput(User.Identity.Name, request.ProjectKey, request.Name, request.DashboardColor, projectApiKey));

            return new CreateProjectResponse
            {
                ProjectKey = request.ProjectKey,
                ProjectApiKey = projectApiKey,
                Name = request.Name,
            };
        }
    }
}