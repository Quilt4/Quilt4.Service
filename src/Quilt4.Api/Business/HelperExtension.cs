using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Quilt4.Api.Business
{
    internal static class HelperExtension
    {
        public static string ToMd5Hash(this string input, string padding)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input + padding);
            var provider = MD5.Create();
            var hash = provider.ComputeHash(inputBytes);
            return hash.Aggregate(string.Empty, (current, b) => current + b.ToString("X2"));
        }
    }
}