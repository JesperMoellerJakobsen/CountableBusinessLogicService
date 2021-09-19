using System.Collections.Generic;
using System.Linq;

namespace Domain.Model.Config
{
    public static class ActionExtensions
    {
        public static bool Any<T>(this IEnumerable<T> list, params T[] enums)
        {
            return enums != null && list.Any(enums.Contains);
        }
    }
}
