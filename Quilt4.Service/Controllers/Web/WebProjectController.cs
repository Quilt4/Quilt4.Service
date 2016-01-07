using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Quilt4.Service.Controllers.Web.DataTransfer;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.Web
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
            return _projectBusiness.GetVersions(null, Guid.Parse(projectId), Guid.Parse(applicationId)).ToProjectPageVersionResponses();
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

        [HttpPost]
        [Authorize]
        [Route("api/project/getUsers")]
        public IEnumerable<QueryUserResponse> GetUsers(QueryUserRequest queryUserRequest)
        {
            if (queryUserRequest == null && string.IsNullOrEmpty(queryUserRequest.SearchString))
                return null;

            var callerIp = HttpContext.Current.Request.UserHostAddress;
            var response = _userBusiness.SearchUsers(queryUserRequest.SearchString, callerIp).Select(x => new QueryUserResponse { UserName = x.Username, EMail = x.Email });
            return response;
        } 
    }
}