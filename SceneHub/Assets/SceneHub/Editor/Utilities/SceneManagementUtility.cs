using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace SceneHub.Editor.Utilities
{
    internal static class SceneManagementUtility
    {
        public static EditorBuildSettingsScene[] BuildScenes
        {
            get => EditorBuildSettings.scenes;
            set => EditorBuildSettings.scenes = value;
        }

        internal static void ChangeScene(string scenePath)
        {
            SaveCurrentScenes();
            EditorSceneManager.OpenScene(scenePath);
        }

        private static void SaveCurrentScenes()
        {
            for (int i = 0; i < EditorSceneManager.sceneCount; i++)
            {
                var scene = EditorSceneManager.GetSceneAt(i);

                if (scene.IsValid() && scene.isDirty)
                {
                    EditorSceneManager.SaveScene(scene);
                }
            }
        }

        internal static void LoadAll(SceneLibraryAsset libraryAsset)
        {
            SaveCurrentScenes();

            if (libraryAsset.IsNullOrInvalid()) return;
            var scenesToLoad = libraryAsset.GetValidScenes().ToList();

            for (var i = 0; i < scenesToLoad.Count; i++)
            {
                EditorSceneManager.OpenScene(scenesToLoad[i], i == 0 ? OpenSceneMode.Single : OpenSceneMode.Additive);
            }
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
            return BuildScenes.Select(x => x.path).Contains(scenePath);
        }

        internal static void AddToBuildList(SceneAsset scene) => AddToBuildList(AssetDatabase.GetAssetPath(scene));

        internal static void AddToBuildList(string scenePath)
        {
            if (IsBuildScene(scenePath)) return;

            var editorBuildScene = new EditorBuildSettingsScene(scenePath, true);
            BuildScenes = BuildScenes.Append(editorBuildScene).ToArray();

            Logger.Log($"Scene '{scenePath}' <b>added</b> to build scene list.");
        }

        internal static void RemoveFromBuildList(SceneAsset scene) => RemoveFromBuildList(AssetDatabase.GetAssetPath(scene));

        internal static void RemoveFromBuildList(string scenePath)
        {
            BuildScenes = BuildScenes.Where(x => x.path != scenePath).ToArray();
            Logger.Log($"Scene '{scenePath}' <b>removed</b> from build scene list.");
        }

        internal static void SetEnabledInBuildList(SceneAsset scene, bool enabled) => SetEnabledInBuildList(AssetDatabase.GetAssetPath(scene), enabled);

        internal static void SetEnabledInBuildList(string scenePath, bool enabled)
        {
            var scenes = BuildScenes.ToArray();

            var buildScene = scenes.FirstOrDefault(x => x.path == scenePath);
            if (buildScene == default) return;

            buildScene.enabled = enabled;

            BuildScenes = scenes;

            Logger.Log($"Scene '{scenePath}' <b>{(enabled ? "enabled" : "disabled")}</b> in build scene list.");
        }

        internal static bool IsEnabledInBuildList(SceneAsset scene) => IsEnabledInBuildList(AssetDatabase.GetAssetPath(scene));

        internal static bool IsEnabledInBuildList(string scenePath)
        {
            var buildScene = BuildScenes.FirstOrDefault(x => x.path == scenePath);
            return buildScene?.enabled ?? false;
        }
    }
}
