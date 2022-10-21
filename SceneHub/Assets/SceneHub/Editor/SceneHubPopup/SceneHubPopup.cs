using System;
using SceneHub.Editor.UserSettings;
using SceneHub.Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup : EditorWindow
    {
        private const string BUILD_LIST_MENU_CATEGORY = "Build list";
        private const string UNITY_MENU = "Tools/Scene Hub/Move To %&q";

        private readonly GUIContent MOVE_AND_PLAY_CONTENT = new GUIContent("▶", "Switch scene and start PlayMode.");
        private readonly GUIContent MENU_CONTENT = new GUIContent("☰", "Additional options for current scene.");

        private readonly GUIContent PING_SCENE_ASSET_CONTENT = new GUIContent("Ping", "Ping scene in Project Tab.");
        private readonly GUIContent ADD_TO_BUILD_SCENE_LIST_CONTENT = new GUIContent($"{BUILD_LIST_MENU_CATEGORY}/Add", "Add to build scene list.");
        private readonly GUIContent REMOVE_FROM_BUILD_SCENE_LIST_CONTENT = new GUIContent($"{BUILD_LIST_MENU_CATEGORY}/Remove", "Remove from build scene list.");
        private readonly GUIContent ENABLE_IN_BUILD_SCENE_LIST_CONTENT = new GUIContent($"{BUILD_LIST_MENU_CATEGORY}/Enable", "Enable scene in build scene list.");
        private readonly GUIContent DISBLE_IN_BUILD_SCENE_LIST_CONTENT = new GUIContent($"{BUILD_LIST_MENU_CATEGORY}/Disable", "Disable scene in build scene list.");

        private Vector2 _scroll;

        private SceneHubSettingsAsset Settings => SceneHubSettingsAsset.instance;

        private PopupTabs SelectedTab
        {
            get => Settings.SelectedTab;
            set => Settings.SelectedTab = value;
        }

        private Vector2 _scroll;

        private static bool IsEditorFree => !EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isCompiling;

        [MenuItem(UNITY_MENU, priority = 47_000)]
        public static void ShowWindow()
        {
            var popup = GetWindow<SceneHubPopup>(true);

            popup.titleContent = new GUIContent("Scene Hub");
            popup.minSize = new Vector2(300, 200);
            popup.Show();

            // show as modal popup

            // var popup = ScriptableObject.CreateInstance(typeof(SceneHubPopup)) as SceneHubPopup;
            // popup.titleContent = new GUIContent("Scene Hub");
            // popup.ShowModalUtility();
        }

        [MenuItem(UNITY_MENU, true)] private static bool ShowValidate() => IsEditorFree;

        private void OnEnable()
        {
            RefreshAll();
        }

        private void OnDisable()
        {
            FavoriteScenesSettings.SaveChanges();
            Settings.SaveChanges();
        }

        private void RefreshAll()
        {
            RefreshLibraries();
            RefreshReferences();
            RefreshScenes();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();

            SelectedTab = EditorGUILayoutUtility.DrawTabToolbar(SelectedTab);

            EditorGUILayout.Space();

            GUI.enabled = IsEditorFree;

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            {
                switch (SelectedTab)
                {
                    case PopupTabs.Libraries:
                        DrawLibraries();
                        break;
                    case PopupTabs.Favorites:
                        DrawFavoriteScenes();
                        break;
                    case PopupTabs.References:
                        DrawReferences();
                        break;
                    case PopupTabs.Scenes:
                        DrawScenes();
                        break;
                    default:
                        DrawLibraries();
                        break;
                }
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            DrawBottomMenu();
        }

        private void DrawBottomMenu()
        {
            if (GUILayout.Button("Refresh All"))
            {
                RefreshAll();
            }
        }

        private void DrawSceneReferenceMenu(ISceneReference sceneReference, string displayName)
        {
            var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(sceneReference?.ScenePath);
            DrawSceneAssetMenu(scene, displayName, sceneReference?.ScenePath);
        }

        private void DrawSceneAssetMenu(SceneAsset scene, string displayName, string tooltip = "")
        {
            var guiState = GUI.enabled;
            GUI.enabled = scene;
            {
                EditorGUILayout.BeginHorizontal();
                {
                    var guiColor = GUI.color;

                    if (GUILayout.Button(new GUIContent(displayName, tooltip)))
                    {
                        Change(scene, false);
                    }

                    GUI.color = Color.green;

                    if (GUILayout.Button(MOVE_AND_PLAY_CONTENT, GUILayout.Width(40f)))
                    {
                        Change(scene, true);
                    }

                    GUI.color = Color.yellow;

                    if (GUILayout.Button(MENU_CONTENT, GUILayout.Width(24f)))
                    {
                        var menu = GetSceneMenu(scene);
                        menu.ShowAsContext();
                    }

                    GUI.color = guiColor;
                }
                EditorGUILayout.EndHorizontal();
            }
            GUI.enabled = guiState;
        }

        private GenericMenu GetSceneMenu(SceneAsset scene)
        {
            var menu = new GenericMenu();

            menu.AddItem(PING_SCENE_ASSET_CONTENT, false, () => EditorGUIUtility.PingObject(scene));

            BuildContextMenuForFavoriteScene(menu, scene);
            BuildContextMenuForBuildScene(menu, scene);

            return menu;
        }

        private void BuildContextMenuForBuildScene(GenericMenu menu, SceneAsset scene)
        {
            var isInBuildList = SceneManagementUtility.IsBuildScene(scene);
            if (isInBuildList)
            {
                menu.AddItem(REMOVE_FROM_BUILD_SCENE_LIST_CONTENT, false, () => SceneManagementUtility.RemoveFromBuildList(scene));

                var enabledInBuildList = SceneManagementUtility.IsEnabledInBuildList(scene);
                if (enabledInBuildList)
                {
                    menu.AddItem(DISBLE_IN_BUILD_SCENE_LIST_CONTENT, false, () => SceneManagementUtility.SetEnabledInBuildList(scene, false));
                }
                else
                {
                    menu.AddItem(ENABLE_IN_BUILD_SCENE_LIST_CONTENT, false, () => SceneManagementUtility.SetEnabledInBuildList(scene, true));
                }
            }
            else
            {
                menu.AddItem(ADD_TO_BUILD_SCENE_LIST_CONTENT, false, () => SceneManagementUtility.AddToBuildList(scene));
            }
        }

        private void Change(SceneAsset scene, bool needStartPlaymode)
        {
            SceneManagementUtility.ChangeScene(scene);
            Close();

            if (needStartPlaymode)
            {
                EditorApplication.isPlaying = true;
            }
        }
    }
}
