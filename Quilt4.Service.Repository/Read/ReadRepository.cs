using System.Linq;
using Quilt4.Service.Interface.Business;
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

        public IDashboardPageProject[] GetDashboardProjects(string userName)
        {
            var response = _dataRepositoryContext.Execute(context =>
            {
                var user = context.Users.Single(x => x.UserName == userName);
                var projectIds = context.UserProjects.Where(x => x.UserId == user.UserId).Select(x => x.ProjectId).ToArray();

                var result = context.DashboardPageProjects.Where(x => projectIds.Contains(x.ProjectId)).ToDashboardProjects().ToArray();
                return result;
            });

            return response;
        }
    }
}