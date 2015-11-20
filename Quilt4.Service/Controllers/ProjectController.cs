﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Controllers
{
    public class ProjectController : ApiController
    {
        private readonly IProjectBusiness _projectBusiness;

        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        [HttpGet]
        public ProjectPageProjectResponse GetProject(string id)
        {
            return _projectBusiness.GetProject(null, Guid.Parse(id)).ToProjectPageProjectResponse();
        }

        [HttpPost]
        [Route("api/project/create")]
        public CreateProjectResponse CreateProject(CreateProjectRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "No request object provided.");

            return new CreateProjectResponse
            {
                ProjectId = _projectBusiness.CreateProject(request.Name, request.DashboardColor).ToString()
            };
        }

        [HttpPost]
        [Route("api/project/update")]
        public UpdateProjectResponse UpdateProject(UpdateProjectRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "No request object provided.");

            return new UpdateProjectResponse
            {
                ProjectId = _projectBusiness.UpdateProject(Guid.Parse(request.Id), request.Name, request.DashboardColor).ToString()
            };
        }


        [Route("api/project/{projectId}/application/{applicationId}/version")]
        public IEnumerable<ProjectPageVersionResponse> GetVersions(string projectId, string applicationId)
        {
            return
                _projectBusiness.GetVersions(null, Guid.Parse(projectId), Guid.Parse(applicationId))
                    .ToProjectPageVersionResponses();
        }
    }

    public class CreateProjectResponse
    {
        public string ProjectId { get; set; }
    }
    public class UpdateProjectResponse
    {
        public string ProjectId { get; set; }
    }

    public class CreateProjectRequest
    {
        public string Name { get; set; }
        public string DashboardColor { get; set; }
    }

    public class UpdateProjectRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DashboardColor { get; set; }
    }
}