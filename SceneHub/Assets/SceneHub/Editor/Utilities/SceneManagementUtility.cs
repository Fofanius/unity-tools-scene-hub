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

            Logger.Log($"Scene '{scenePath}' <b>added</b> to build scene list.");
        }

        internal static void RemoveFromBuildList(SceneAsset scene) => RemoveFromBuildList(AssetDatabase.GetAssetPath(scene));

        internal static void RemoveFromBuildList(string scenePath)
        {
            EditorBuildSettings.scenes = EditorBuildSettings.scenes.Where(x => x.path != scenePath).ToArray();
            Logger.Log($"Scene '{scenePath}' <b>removed</b> from build scene list.");
        }

        internal static void SetEnabledInBuildList(SceneAsset scene, bool enabled) => SetEnabledInBuildList(AssetDatabase.GetAssetPath(scene), enabled);

        internal static void SetEnabledInBuildList(string scenePath, bool enabled)
        {
            var scenes = EditorBuildSettings.scenes.ToArray();

            var buildScene = scenes.FirstOrDefault(x => x.path == scenePath);
            if (buildScene == default) return;

            buildScene.enabled = enabled;

            EditorBuildSettings.scenes = scenes;

            Logger.Log($"Scene '{scenePath}' <b>{(enabled ? "enabled" : "disabled")}</b> in build scene list.");
        }

        internal static bool IsEnabledInBuildList(SceneAsset scene) => IsEnabledInBuildList(AssetDatabase.GetAssetPath(scene));

        internal static bool IsEnabledInBuildList(string scenePath)
        {
            var buildScene = EditorBuildSettings.scenes.FirstOrDefault(x => x.path == scenePath);
            return buildScene?.enabled ?? false;
        }
    }
}
