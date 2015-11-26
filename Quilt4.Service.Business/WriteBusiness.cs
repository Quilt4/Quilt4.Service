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
        private bool _running;
        private static AutoResetEvent _autoResetEvent = new AutoResetEvent(false);

        public WriteBusiness(IWriteRepository repository)
        {
            _repository = repository;
            Task.Factory.StartNew(Run);
            _running = true;
        }

        private void Run()
        {
            while (_running)
            {
                _autoResetEvent.WaitOne();
                try
                {
                    _repository.WriteToReadDb();
                }
                catch (Exception e)
                {
                    //TODO: Log and handle exception
                }
            }
        }

        public static void RunRecalculate()
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