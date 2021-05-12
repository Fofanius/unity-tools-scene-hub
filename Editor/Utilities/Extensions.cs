using System;
using System.Collections.Generic;
using System.Linq;

namespace SceneHub.Utilities
{
    internal static class Extensions
    {
        internal static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || collection.Count() == default;

        internal static string GetLibraryDisplayName(this SceneLibrary library)
        {
            if (library)
            {
                return string.IsNullOrWhiteSpace(library.Title) ? $"[{library.name}]" : $"{library.Title} [{library.name}]";
            }

            throw new ArgumentNullException(nameof(library));
        }

        internal static string GetSceneInfoDisplayName(this SceneInfo sceneInfo)
        {
            if (sceneInfo == null) throw new ArgumentNullException(nameof(sceneInfo));
            return string.IsNullOrWhiteSpace(sceneInfo.Title) ? $"[{sceneInfo.SceneName}]" : $"{sceneInfo.Title} [{sceneInfo.SceneName}]";
        }
    }
}
