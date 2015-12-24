using System;

namespace Quilt4.Service.Business
{
    static class ValidationHelper
    {
        public static bool IsValidGuid(this Guid item)
        {
            return item.ToString().IsValidGuid();
        }

        public static bool IsValidGuid(this string item)
        {
            Guid gd;
            if (!Guid.TryParse(item, out gd))
                return false;

            if (gd == Guid.Empty)
                return false;

            return true;
        }

        public static string SetIfEmpty(this string item, string value)
        {
            return string.IsNullOrEmpty(item) ? value : item;
        }
    }
}