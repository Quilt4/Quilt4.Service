using System;
using Quilt4.Service.SqlRepository.Interface;
using Quilt4.Service.SqlRepository.Read;

namespace Quilt4.Service.SqlRepository.Data
{
    public class DataRepositoryContext : IWriteContext<DataDataContext>
    {
        public void Execute(Action<DataDataContext> action)
        {
            using (var context = new DataDataContext(ConnectionStringHelper.GetConnectionString("DataConnection")))
            {
                action(context);
            }
        }

        public T Execute<T>(Func<DataDataContext, T> func)
        {
            T response;
            using (var context = new DataDataContext(ConnectionStringHelper.GetConnectionString("DataConnection")))
            {
                context.ObjectTrackingEnabled = false;
                response = func(context);
            }

            return response;
        }
    }
}