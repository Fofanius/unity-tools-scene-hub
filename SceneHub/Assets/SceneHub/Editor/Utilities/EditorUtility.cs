using UnityEditor;

namespace SceneHub.Editor.Utilities
{
    internal static class EditorUtility
    {
        internal static bool IsEditorFree => !EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isCompiling;

        internal static void StartPlayMode()
        {
            EditorApplication.isPlaying = true;
        }
    }
}
