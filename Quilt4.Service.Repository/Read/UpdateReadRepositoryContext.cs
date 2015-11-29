using System;
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
    }
}