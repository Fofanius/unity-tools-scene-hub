using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace SceneHub.Editor.Utilities
{
    internal static class SceneManagementUtility
    {
        /// <summary>
        /// Сохраняет текущую активную сцену и осуществляет переход на заданную.
        /// </summary>
        internal static void ChangeScene(string scenePath)
        {
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            EditorSceneManager.OpenScene(scenePath);
        }

        internal static void ChangeScene(SceneAsset scene)
        {
            ChangeScene(AssetDatabase.GetAssetPath(scene));
        }

        internal static bool IsBuildScene(SceneAsset scene)
        {
            return IsBuildScene(AssetDatabase.GetAssetPath(scene));
        }

        internal static bool IsBuildScene(string scenePath)
        {
            return EditorBuildSettings.scenes.Select(x => x.path).Contains(scenePath);
        }

        internal static void AddToBuildList(SceneAsset scene) => AddToBuildList(AssetDatabase.GetAssetPath(scene));

        internal static void AddToBuildList(string scenePath)
        {
            if (IsBuildScene(scenePath)) return;

            var editorBuildScene = new EditorBuildSettingsScene(scenePath, true);
            EditorBuildSettings.scenes = EditorBuildSettings.scenes.Append(editorBuildScene).ToArray();
        }

        internal static void RemoveFromBuildList(SceneAsset scene) => RemoveFromBuildList(AssetDatabase.GetAssetPath(scene));

        internal static void RemoveFromBuildList(string scenePath)
        {
            EditorBuildSettings.scenes = EditorBuildSettings.scenes.Where(x => x.path != scenePath).ToArray();
        }
    }
}
