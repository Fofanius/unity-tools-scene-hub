using System.Collections.Generic;
using System.Linq;

namespace SceneHub.Utilities
{
    internal static class Extensions
    {
        internal static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || collection.Count() == default;
    }
}
