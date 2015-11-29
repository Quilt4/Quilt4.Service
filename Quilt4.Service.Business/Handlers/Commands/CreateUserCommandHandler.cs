using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Commands
{
    public class CreateUserCommandHandler : CommandHandlerBase<ICrateUserCommandInput>
    {
        public CreateUserCommandHandler(IDataRepository repository, IUpdateReadRepository writeRepository)
            : base(repository, writeRepository)
        {
        }

        protected override void DoHandle(ICrateUserCommandInput input)
        {
            Repository.CreateUser(input.UserName);
            WriteRepository.CreateUser(input.UserName);
        }
    }
}