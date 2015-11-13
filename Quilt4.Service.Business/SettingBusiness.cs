using Quil4.Service.Interface.Business;
using Quil4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class SettingBusiness : ISettingBusiness
    {
        private static readonly object SyncRoot = new object();
        private readonly IRepository _repository;

        public SettingBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public string GetPasswordPadding()
        {
            lock (SyncRoot)
            {
                var response = _repository.GetSetting<string>("PasswordPadding");
                if (string.IsNullOrEmpty(response))
                {
                    response = RandomUtility.GetRandomString(20);
                    _repository.SetSetting("PasswordPadding", response);
                }
                return response;
            }
        }
    }
}