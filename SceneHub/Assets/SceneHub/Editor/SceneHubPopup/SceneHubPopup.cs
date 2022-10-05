using SceneHub.Editor.UserSettings;
using SceneHub.Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup : EditorWindow
    {
        private readonly GUIContent MOVE_AND_PLAY_CONTENT = new GUIContent("▶", "Switch scene and start PlayMode.");
        private readonly GUIContent MENU_CONTENT = new GUIContent("☰", "Additional options for current scene.");

        private readonly GUIContent PING_SCENE_ASSET_CONTENT = new GUIContent("Ping", "Ping scene in Project Tab.");
        private readonly GUIContent ADD_TO_BUILD_SCENE_LIST_CONTENT = new GUIContent("Build list/Add", "Add to build scene list.");
        private readonly GUIContent REMOVE_FROM_BUILD_SCENE_LIST_CONTENT = new GUIContent("Build list/Remove", "Remove from build scene list.");
        private readonly GUIContent ADD_TO_FAVORITE_SCENE_ASSET_CONTENT = new GUIContent("Favorite/Add", "Add to favorite list.");
        private readonly GUIContent REMOVE_FROM_FAVORITE_SCENE_ASSET_CONTENT = new GUIContent("Favorite/Remove", "Remove from favorite list.");

        private PopupTabs SelectedTab
        {
            get => SceneHubCommonCacheAsset.instance.SelectedTab;
            set => SceneHubCommonCacheAsset.instance.SelectedTab = value;
        }

        private Vector2 _scroll;

        private static bool IsEditorFree => !EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isCompiling;

        [MenuItem("Scene Hub/Move To %&q", priority = 100)]
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

        [MenuItem("Scene Hub/Move To %&q", true)] private static bool ShowValidate() => IsEditorFree;

        private void OnEnable()
        {
            RefreshAll();
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

        private void DrawSceneReferenceMenu(ISceneReference sceneReference, string displayName, Color color)
        {
            var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(sceneReference?.ScenePath);
            DrawSceneAssetMenu(scene, displayName, color);
        }

        private void DrawSceneAssetMenu(SceneAsset scene, string displayName, Color color)
        {
            var guiState = GUI.enabled;
            GUI.enabled = scene;
            {
                EditorGUILayout.BeginHorizontal();
                {
                    var guiColor = GUI.color;

                    GUI.color = guiColor * color;
                    if (GUILayout.Button(displayName))
                    {
                        Change(scene, false);
                    }

                    GUI.color = Color.green;

                    if (GUILayout.Button(MOVE_AND_PLAY_CONTENT, GUILayout.Width(40f)))
                    {
                        Change(scene, true);
                    }

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

            var isFavorite = IsFavorite(scene);
            if (isFavorite)
            {
                menu.AddDisabledItem(ADD_TO_FAVORITE_SCENE_ASSET_CONTENT, false);
                menu.AddItem(REMOVE_FROM_FAVORITE_SCENE_ASSET_CONTENT, false, () => RemoveFromFavorite(scene));
            }
            else
            {
                menu.AddItem(ADD_TO_FAVORITE_SCENE_ASSET_CONTENT, false, () => AddToFavorite(scene));
                menu.AddDisabledItem(REMOVE_FROM_FAVORITE_SCENE_ASSET_CONTENT, false);
            }

            var isInBuildList = SceneManagementUtility.IsBuildScene(scene);

            if (isInBuildList)
            {
                menu.AddDisabledItem(ADD_TO_BUILD_SCENE_LIST_CONTENT, false);
                menu.AddItem(REMOVE_FROM_BUILD_SCENE_LIST_CONTENT, false, () => SceneManagementUtility.RemoveFromBuildList(scene));
            }
            else
            {
                menu.AddItem(ADD_TO_BUILD_SCENE_LIST_CONTENT, false, () => SceneManagementUtility.AddToBuildList(scene));
                menu.AddDisabledItem(REMOVE_FROM_BUILD_SCENE_LIST_CONTENT, false);
            }

            return menu;
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
