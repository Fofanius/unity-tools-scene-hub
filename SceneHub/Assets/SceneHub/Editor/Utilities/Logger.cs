using UnityEngine;

namespace SceneHub.Editor.Utilities
{
    internal static class Logger
    {
        internal static void Log(string message)
        {
            Debug.Log($"<b>[SceneHUB]</b> {message}");
        }
    }
}
