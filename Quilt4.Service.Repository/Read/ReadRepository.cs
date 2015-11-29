using System.Linq;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.SqlRepository.Converter;
using Quilt4.Service.SqlRepository.Data;

namespace Quilt4.Service.SqlRepository.Read
{
    public class ReadRepository : IReadRepository
    {
        private readonly ReadOnlyRepositoryContext _dataRepositoryContext;

        public ReadRepository(ReadOnlyRepositoryContext dataRepositoryContext)
        {
            _dataRepositoryContext = dataRepositoryContext;
        }

        public Entity.DashboardPageProject[] GetDashboardProjects(string userName)
        {
            var response = _dataRepositoryContext.Execute(context =>
            {
                return context.DashboardPageProjects.Where(x => x.UserProjects.Any(y => y.User.UserName == userName)).ToDashboardProjects().ToArray();
            });

            return response;
        }
    }
}