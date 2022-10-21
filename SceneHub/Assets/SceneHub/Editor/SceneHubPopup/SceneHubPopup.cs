using SceneHub.Editor.UserSettings;
using SceneHub.Editor.Utilities;
using UnityEditor;
using UnityEngine;
using EditorUtility = SceneHub.Editor.Utilities.EditorUtility;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup : EditorWindow
    {
        private Vector2 _scroll;

        private SceneHubSettingsAsset Settings => SceneHubSettingsAsset.instance;

        private PopupTabs SelectedTab
        {
            get => Settings.SelectedTab;
            set => Settings.SelectedTab = value;
        }

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

            GUI.enabled = EditorUtility.IsEditorFree;

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
                        Change(scene);
                    }

                    GUI.color = Color.green;

                    if (GUILayout.Button(SceneHubGUIContent.SceneOptions.SwitchSceneAndPlay, GUILayout.Width(40f)))
                    {
                        ChangeAndStartPlayMode(scene);
                    }

                    GUI.color = Color.yellow;

                    if (GUILayout.Button(SceneHubGUIContent.SceneOptions.ContextMenu, GUILayout.Width(24f)))
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

        private void Change(SceneAsset scene)
        {
            SceneManagementUtility.ChangeScene(scene);
            Close();
        }

        private void ChangeAndStartPlayMode(SceneAsset scene)
        {
            Change(scene);
            EditorUtility.StartPlayMode();
        }
    }
}
