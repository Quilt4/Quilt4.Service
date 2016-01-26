using System.Security.Cryptography;
using System.Text;

namespace Quilt4.Service.Business
{
    internal static class GravatarHelper
    {
        public static string GetGravatarPath(this string email)
        {
            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(email));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            var response = sBuilder.ToString();           

            return "http://www.gravatar.com/avatar/" + response;
        }
    }
}