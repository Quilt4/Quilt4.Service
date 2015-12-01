﻿using System;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Entity
{
    public class GetProjectsQueryInput : IGetProjectsQueryInput
    {
        public GetProjectsQueryInput(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }

    public class GetProjectQueryInput : IGetProjectQueryInput
    {
        public GetProjectQueryInput(string userName, Guid projectKey)
        {
            ProjectKey = projectKey;
            UserName = userName;
        }

        public string UserName { get; }
        public Guid ProjectKey { get; }
    }
}