using System;
using System.Collections.Generic;
using System.Linq;

namespace SceneHub.Utilities
{
    internal static class Extensions
    {
        internal static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || collection.Count() == default;

        internal static string GetLibraryDisplayName(this SceneLibraryAsset libraryAsset)
        {
            if (libraryAsset)
            {
                return string.IsNullOrWhiteSpace(libraryAsset.Title) ? $"[{libraryAsset.name}]" : $"{libraryAsset.Title} [{libraryAsset.name}]";
            }

            throw new ArgumentNullException(nameof(libraryAsset));
        }

        internal static string GetSceneInfoDisplayName(this SceneReference sceneReference)
        {
            if (sceneReference == null) throw new ArgumentNullException(nameof(sceneReference));
            return string.IsNullOrWhiteSpace(sceneReference.Title) ? $"[{sceneReference.ScenePath}]" : $"{sceneReference.Title} [{sceneReference.ScenePath}]";
        }
    }
}
