//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Web.Http;
//using Quilt4.Service.Converters;
//using Quilt4.Service.DataTransfer;
//using Quilt4.Service.Entity;
//using Quilt4.Service.Interface.Business;

//namespace Quilt4.Service.Controllers
//{
//    public class ProjectController : ApiController
//    {
//        private readonly IProjectBusiness _projectBusiness;

//        public ProjectController(IProjectBusiness projectBusiness)
//        {
//            _projectBusiness = projectBusiness;
//        }

//        //[HttpGet]
//        //public ProjectPageProjectResponse GetProject(string id)
//        //{
//        //    return _projectBusiness.GetProject(null, Guid.Parse(id)).ToProjectPageProjectResponse();
//        //}
        
//        //[HttpGet]
//        //public ProjectPageProjectResponse[] GetProjects()
//        //{
//        //    //TODO: Move this so that it is applied for all calls.
//        //    AuthenticateCall();

//        //    return new[] { new ProjectPageProjectResponse { Name = "A", } };
//        //}

//        [HttpPost]
//        [Route("api/project/create")]
//        public CreateProjectResponse CreateProject(CreateProjectRequest request)
//        {
//            if (request == null)
//                throw new ArgumentNullException(nameof(request), "No request object provided.");

//            //TODO: Move this so that it is applied for all calls.
//            AuthenticateCall(request);

//            _projectBusiness.CreateProject(request.ProjectKey, request.Name, request.DashboardColor);

//            return new CreateProjectResponse();
//        }

//        //TODO: Move this so that it is applied for all calls.
//        private void AuthenticateCall(object request = null)
//        {
//            if (!string.IsNullOrEmpty(Request.Headers.Authorization.Scheme))
//            {
//                var header = Encoding.UTF8.GetString(Convert.FromBase64String(Request.Headers.Authorization.Parameter)).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
//                var publicSessionKey = header[0];
//                var messageHash = header[1];

//                var securityType = (SecurityType)Enum.Parse(typeof(SecurityType), Request.Headers.Authorization.Scheme);
//                switch (securityType)
//                {
//                    case SecurityType.Simple:
//                        //TODO: Compare the URL called and the data provided
//                        var requestedUrl = Request.RequestUri;
//                        //var content = Request.Content.ReadAsStringAsync().Result;

//                        break;
//                    default:
//                        throw new ArgumentOutOfRangeException(string.Format("Unknown security type {0}.", securityType));
//                }
//            }
//        }

//        [HttpPost]
//        [Route("api/project/update")]
//        public UpdateProjectResponse UpdateProject(UpdateProjectRequest request)
//        {
//            if (request == null)
//                throw new ArgumentNullException(nameof(request), "No request object provided.");

//            _projectBusiness.UpdateProject(Guid.Parse(request.Id), request.Name, request.DashboardColor);

//            return new UpdateProjectResponse();
//        }

//        [Route("api/project/{projectId}/application/{applicationId}/version")]
//        public IEnumerable<ProjectPageVersionResponse> GetVersions(string projectId, string applicationId)
//        {
//            return _projectBusiness.GetVersions(null, Guid.Parse(projectId), Guid.Parse(applicationId)) .ToProjectPageVersionResponses();
//        }
//    }

//    //TODO: Move the definition to the toolkit (
//    public class CreateProjectResponse
//    {
//    }

//    public class UpdateProjectResponse
//    {
//    }

//    public class CreateProjectRequest
//    {
//        public Guid ProjectKey { get; set; }
//        public string Name { get; set; }
//        public string DashboardColor { get; set; }
//    }

//    public class UpdateProjectRequest
//    {
//        public string Id { get; set; }
//        public string Name { get; set; }
//        public string DashboardColor { get; set; }
//    }
//}