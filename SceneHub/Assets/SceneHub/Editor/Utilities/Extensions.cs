using System.Collections.Generic;

namespace SceneHub.Utilities
{
    internal static class Extensions
    {
        internal static bool IsNullOrEmpty<T>(this ICollection<T> collection) => collection == null || collection.Count == default;
    }
}