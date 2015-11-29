using System;
using Quilt4.Service.SqlRepository.Read;

namespace Quilt4.Service.SqlRepository.Data
{
    public class ReadOnlyRepositoryContext : IReadOnlyContext<ReadDataContext>
    {
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