using System;
using System.Threading;
using System.Threading.Tasks;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class WriteBusiness : IWriteBusiness, IDisposable
    {
        private readonly IWriteRepository _repository;
        private readonly IServiceLog _serviceLog;
        private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private bool _running;

        public WriteBusiness(IWriteRepository repository, IServiceLog serviceLog)
        {
            _repository = repository;
            _serviceLog = serviceLog;
            Task.Factory.StartNew(Run);
            _running = true;
        }

        private void Run()
        {
            while (_running)
            {
                _autoResetEvent.WaitOne(5 * 60 * 1000); //Timeout after 5 minutes
                try
                {
                    _repository.WriteToReadDb();
                }
                catch (Exception e)
                {
                    _serviceLog.LogException(e);
                }
            }
        }

        public void RunRecalculate()
        {
            _autoResetEvent.Set();
        }

        public void Dispose()
        {
            _running = false;
            _autoResetEvent.Dispose();
        }
    }
}