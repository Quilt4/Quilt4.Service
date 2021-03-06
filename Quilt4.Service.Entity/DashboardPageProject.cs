﻿using System;

namespace Quilt4.Service.Entity
{
    public class DashboardPageProject
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public int Applications { get; set; }
        public int Versions { get; set; }
        public int Sessions { get; set; }
        public int IssueTypes { get; set; }
        public int Issues { get; set; }
        public string DashboardColor { get; set; }
    }
}