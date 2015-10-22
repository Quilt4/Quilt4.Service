using Quilt4.Api.Business;
using Quilt4.Api.Interfaces;
using Quilt4.Api.Repositories;

namespace Quilt4.Api
{
    public class CompositeRoot
    {
        private CompositeRoot()
        {
            UserBusiness = new UserBusiness(new MemoryRepository());
        }

        public static CompositeRoot Instance => new CompositeRoot();

        public IUserBusiness UserBusiness { get; }
    }
}
