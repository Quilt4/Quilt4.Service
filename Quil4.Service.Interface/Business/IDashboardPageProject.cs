﻿using System;

namespace Quilt4.Service.Interface.Business
{
    public interface IDashboardPageProject
    {
        Guid ProjectKey { get; }
        string Name { get; }
        int VersionCount { get; }
        int SessionCount { get; }
        int IssueTypeCount { get; }
        int IssueCount { get; }
        string DashboardColor { get; }
    }
}