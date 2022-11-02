using System.Linq;
using SceneHub.Editor.Utilities;
using UnityEditor;
using UnityEngine;
using Logger = SceneHub.Editor.Utilities.Logger;

namespace SceneHub.Editor
{
    internal static class SceneHubEditorMenu
    {
        /*
         * https://docs.unity3d.com/ScriptReference/MenuItem.html
         * 
         * % (ctrl on Windows and Linux, cmd on macOS)
         * ^ (ctrl on Windows, Linux, and macOS)
         * # (shift)
         * & (alt)
         * _ (no modifiers)
         */

        private const string SCENE_HUB_POPUP_EDITOR_MENU_PATH = "Tools/Scene Hub/Move To %&q";
        private const string START_PLAY_FROM_FIRST_BUILD_SCENE_EDITOR_MENU_PATH = "Tools/Scene Hub/Start Play Mode #&p";

        [MenuItem(SCENE_HUB_POPUP_EDITOR_MENU_PATH, priority = 0)]
        private static void OpenSceneHubPopup()
        {
            var popup = EditorWindow.GetWindow<SceneHubPopup>(true);

            popup.titleContent = new GUIContent("Scene Hub");
            popup.minSize = new Vector2(300, 200);
            popup.Show();
        }

        [MenuItem(SCENE_HUB_POPUP_EDITOR_MENU_PATH, true)]
        private static bool OpenSceneHubPopupValidation() => Utilities.EditorUtility.IsEditorFree;

        [MenuItem(START_PLAY_FROM_FIRST_BUILD_SCENE_EDITOR_MENU_PATH, priority = 100)]
        public static void StartPlayModeFromFirstBuildScene()
        {
            var firstEnabled = SceneManagementUtility.BuildScenes.FirstOrDefault(x => x.enabled);
            if (firstEnabled == null)
            {
                Logger.LogWarning($"There is no active scenes in Build List!");
            }
            else
            {
                SceneManagementUtility.ChangeScene(firstEnabled.path);
                Utilities.EditorUtility.StartPlayMode();
            }
        }

        [MenuItem(START_PLAY_FROM_FIRST_BUILD_SCENE_EDITOR_MENU_PATH, true)]
        public static bool StartPlayModeFromFirstBuildSceneValidation() => Utilities.EditorUtility.IsEditorFree;
    }
}
