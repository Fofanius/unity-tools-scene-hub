using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace SceneHub.Utilities
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

        internal static void ToggleBuildStatus(SceneAsset scene)
        {
            ToggleBuildStatus(AssetDatabase.GetAssetPath(scene));
        }

        internal static void ToggleBuildStatus(string scenePath)
        {
            if (EditorBuildSettings.scenes.Select(x => x.path).Contains(scenePath))
            {
                EditorBuildSettings.scenes = EditorBuildSettings.scenes.Where(x => x.path != scenePath).ToArray();
            }
            else
            {
                var editorBuildScene = new EditorBuildSettingsScene(scenePath, true);
                EditorBuildSettings.scenes = EditorBuildSettings.scenes.Append(editorBuildScene).ToArray();
            }

            AssetDatabase.SaveAssets();
        }
    }
}
