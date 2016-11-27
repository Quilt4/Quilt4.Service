using Quilt4.Service.Entity;

namespace Quilt4.Service.Business.Converters
{
    internal static class Converter
    {
        public static UserInfo ToUserInfo(this UserInfo x)
        {
            return new UserInfo(x.UserKey, x.Username, x.Email, x.FullName, x.Email.GetGravatarPath(), x.Roles);
        }
    }
}
