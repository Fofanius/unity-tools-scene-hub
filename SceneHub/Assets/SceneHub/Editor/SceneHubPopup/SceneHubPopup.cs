using System;
using SceneHub.Utilities;
using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup : EditorWindow
    {
        [Serializable]
        private enum PopupTabs
        {
            Libraries,
            ReferenceAssets,
            Scenes
        }

        private readonly string[] TOOL_BAR_TABS = new[] { PopupTabs.Libraries.ToString(), PopupTabs.ReferenceAssets.ToString(), PopupTabs.Scenes.ToString(), };

        private readonly GUIContent MOVE_AND_PLAY_CONTENT = new GUIContent("Play", "Switch scene and start PlayMode.");
        private readonly GUIContent PING_SCENE_ASSET_CONTENT = new GUIContent("P", "Ping in project tab.");
        private readonly GUIContent BUILD_SCENE_ASSET_CONTENT = new GUIContent("B", "Add/Remove from build scenes list.");

        [SerializeField] private PopupTabs _selectedTab;

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

            _selectedTab = (PopupTabs)GUILayout.Toolbar((int)_selectedTab, TOOL_BAR_TABS);

            EditorGUILayout.Space();

            GUI.enabled = IsEditorFree;

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            {
                switch (_selectedTab)
                {
                    case PopupTabs.Libraries:
                        DrawLibraries();
                        break;

                    case PopupTabs.ReferenceAssets:
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

                    GUI.color = Color.yellow;
                    if (GUILayout.Button(BUILD_SCENE_ASSET_CONTENT, GUILayout.Width(18f)))
                    {
                        SceneManagementUtility.ToggleBuildStatus(scene);
                    }

                    GUI.color = Color.cyan;
                    if (GUILayout.Button(PING_SCENE_ASSET_CONTENT, GUILayout.Width(18f)))
                    {
                        EditorGUIUtility.PingObject(scene);
                    }

                    GUI.color = Color.green;

                    if (GUILayout.Button(MOVE_AND_PLAY_CONTENT, GUILayout.Width(40f)))
                    {
                        Change(scene, true);
                    }

                    GUI.color = guiColor;
                }
                EditorGUILayout.EndHorizontal();
            }
            GUI.enabled = guiState;
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
