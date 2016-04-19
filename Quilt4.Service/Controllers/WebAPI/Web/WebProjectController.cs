using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Quilt4.Service.Controllers.WebAPI.Web.DataTransfer;
using Quilt4.Service.Converters;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.WebAPI.Web
{
    [Route("api/Web/Project")]
    public class WebProjectController : ApiController
    {
        private readonly IProjectBusiness _projectBusiness;
        private readonly IUserBusiness _userBusiness;

        public WebProjectController(IProjectBusiness projectBusiness, IUserBusiness userBusiness)
        {
            _projectBusiness = projectBusiness;
            _userBusiness = userBusiness;
        }

        [HttpGet]
        [Authorize]
        [Route("api/project/{projectId}")]
        public ProjectPageProjectResponse GetProject(string projectId)
        {
            return _projectBusiness.GetProject(User.Identity.Name, Guid.Parse(projectId)).ToProjectPageProjectResponse();
        }

        [HttpGet]
        [Authorize]
        [Route("api/project/{projectId}/application/{applicationId}/version")]
        public IEnumerable<ProjectPageVersionResponse> GetVersions(string projectId, string applicationId)
        {
            var response  = _projectBusiness.GetVersions(User.Identity.Name, Guid.Parse(applicationId)).ToProjectPageVersionResponses().ToArray();
            return response;
        }

        [HttpPost]
        [Authorize]
        [Route("api/project/create")]
        public void CreateProject(ProjectInput request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");

            _projectBusiness.CreateProject(User.Identity.Name, request.ProjectKey, request.Name, request.DashboardColor);
        }

        [HttpPost]
        [Authorize]
        [Route("api/project/update")]
        public void UpdateProject(ProjectInput request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");

            _projectBusiness.UpdateProject(request.ProjectKey, request.Name, request.DashboardColor, User.Identity.Name);
        }

        [HttpGet]
        [Authorize]
        [Route("api/project/{projectId}/members")]
        public IEnumerable<ProjectMember> GetMembers(string projectId)
        {
            var members = _projectBusiness.GetMembers(Guid.Parse(projectId));

            return members;
        }

        //TODO: Change response type not to be "ProjectMember" but some other more specific type. (Can QueryUserResponse be used?)
        [HttpPost]
        [Authorize]
        [Route("api/project/getUsers")]
        public IEnumerable<ProjectMember> GetUsers(GetUsersRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email))
                return null;

            var callerIp = HttpContext.Current.Request.UserHostAddress;
            var members = _userBusiness.SearchUsers(request.Email, callerIp).Select(x => new ProjectMember(x.Username, x.Email, false, null, x.FullName, x.AvatarUrl));

            return members;
        } 

    }

    public class GetUsersRequest
    {
        public string Email { get; set; }
    }
}