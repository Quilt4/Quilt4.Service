using System;
using Quilt4.Service.SqlRepository.Interface;
using Quilt4.Service.SqlRepository.Read;

namespace Quilt4.Service.SqlRepository.Data
{
    public class UpdateReadRepositoryContext : IWriteContext<ReadDataContext>
    {
        public void Execute(Action<ReadDataContext> action)
        {
            using (var context = new ReadDataContext(ConnectionStringHelper.GetConnectionString("ReadConnection")))
            {
                action(context);
            }
        }

        public T Execute<T>(Func<ReadDataContext, T> func)
        {
            T response;
            using (var context = new ReadDataContext(ConnectionStringHelper.GetConnectionString("ReadConnection")))
            {
                context.ObjectTrackingEnabled = false;
                response = func(context);
            }

            return response;
        }
    }
}