using System;

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
    }
}