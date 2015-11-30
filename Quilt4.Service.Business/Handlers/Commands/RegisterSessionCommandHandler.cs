using System;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business.Handlers.Commands
{
    public class RegisterSessionCommandHandler : CommandHandlerBase<IRegisterSessionCommandInput>
    {
        public RegisterSessionCommandHandler(IDataRepository repository, IUpdateReadRepository writeRepository)
            : base(repository, writeRepository)
        {
        }

        protected override void DoHandle(IRegisterSessionCommandInput input)
        {
            throw new NotImplementedException();
        }
    }
}