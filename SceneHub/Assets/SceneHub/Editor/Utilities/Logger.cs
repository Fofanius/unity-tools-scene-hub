using UnityEngine;

namespace SceneHub.Editor.Utilities
{
    internal static class Logger
    {
        private static string FormatMessage(string message)
        {
            return $"<b>[Scene HUB]</b> {message}";
        }

        internal static void Log(string message)
        {
            Debug.Log(FormatMessage(message));
        }

        internal static void LogWarning(string message)
        {
            Debug.LogWarning(FormatMessage(message));
        }
    }
}
