using System;
using System.Linq;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class ReadModelBusiness : IReadModelBusiness, IDisposable
    {
        private readonly IRepository _repository;
        private readonly IProjectRepository _projectRepository;
        //private readonly IReadRepository _readRepository;

        public ReadModelBusiness(IRepository repository, IProjectRepository projectRepository)
        {
            _repository = repository;
            _projectRepository = projectRepository;
            //_readRepository = readRepository;
        }

        public void Dispose()
        {
        }

        public void AddIssue(Guid issueKey)
        {
            var issue = _repository.GetIssue(issueKey);
            var session = _repository.GetSession(issue.SessionKey);
            var project = _projectRepository.GetAllProjects().Single();
            var application = _repository.GetApplications(session.ProjectKey).Single();
            var version = _repository.GetVersions(application.ApplicationKey).Single();
            var issueType = _repository.GetIssueTypes(session.VersionKey).Single();
            //_repository.mach

            var issueRead = new IssueRead
            {
                IssueKey = issue.IssueKey,
                Ticket = issue.Ticket,
                ApplicationName = application.Name,
                VersionNumber = version.VersionNumber,
                Message = issueType.Message,
                Type = issueType.Type,
                StackTrace = issueType.StackTrace,
                Level = issueType.Level,
                ProjectName = project.Name,
                MachineName = "?",
            };
            //_readRepository.AddIssue(issueRead);
        }

        public IIssueRead GetIssue(Guid issueKey)
        {
            throw new NotImplementedException();
            //return _readRepository.GetIssue(issueKey);
        }
    }

    //public class WriteBusiness : IWriteBusiness, IDisposable
    //{
    //    private readonly IWriteRepository _repository;
    //    private readonly IServiceLog _serviceLog;
    //    //private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
    //    //private bool _running;

    //    public WriteBusiness(IWriteRepository repository, IServiceLog serviceLog)
    //    {
    //        _repository = repository;
    //        _serviceLog = serviceLog;
    //        //_running = true;
    //        //Task.Factory.StartNew(Run);
    //    }

    //    //private void Run()
    //    //{
    //    //    while (_running)
    //    //    {
    //    //        _autoResetEvent.WaitOne(5 * 60 * 1000); //Timeout after 5 minutes
    //    //        try
    //    //        {
    //    //            _repository.WriteToReadDb();
    //    //        }
    //    //        catch (Exception e)
    //    //        {
    //    //            _serviceLog.LogException(e, LogLevel.SystemError);
    //    //        }
    //    //    }
    //    //}

    //    public void RunRecalculate()
    //    {
    //        //_repository.WriteToReadDb();
    //        //_autoResetEvent.Set();
    //    }

    //    public void Dispose()
    //    {
    //        //_running = false;
    //        //_autoResetEvent.Dispose();
    //    }

    //    public void AddIssue(Guid issuekey)
    //    {
    //        //TODO: Handle
    //    }
    //}
}