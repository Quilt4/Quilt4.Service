using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.User.DataTransfer
{
    internal class CrateUserCommandInput : ICrateUserCommandInput
    {
        public CrateUserCommandInput(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}