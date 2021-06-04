using System;
using System.Collections.Generic;
using System.Linq;
using SceneHub.Utilities;
using UnityEditor;
using UnityEngine;

namespace SceneHub
{
    public class SceneHubPopup : EditorWindow
    {
        private readonly GUIContent MOVE_AND_PLAY_CONTENT = new GUIContent("Play", "Switch scene and start PlayMode.");
        private readonly GUIContent PING_SCENE_ASSET_CONTENT = new GUIContent("P", "Ping in project tab.");
        private readonly GUIContent BUILD_SCENE_ASSET_CONTENT = new GUIContent("B", "Add/Remove from build scenes list.");

        private List<SceneLibrary> _assets;
        private IEnumerable<SceneAsset> _otherScenes;
        private Vector2 _scroll;

        private static bool IsEditorFree => !EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isCompiling;

        [MenuItem("Scene Hub/Move To %&q", priority = 100)]
        public static void ShowWindow()
        {
            var window = GetWindow<SceneHubPopup>();

            window.titleContent = new GUIContent("Change Scene");
            window.ShowPopup();
        }

        [MenuItem("Scene Hub/Move To %&q", true)] private static bool ShowValidate() => IsEditorFree;

        private void OnEnable()
        {
            RefreshAssetList();
            RefreshNonAssetSceneList();

            minSize = new Vector2(300, 200);
        }

        private void RefreshNonAssetSceneList()
        {
            var set = new HashSet<SceneAsset>(_assets.SelectMany(x => x.Scenes.Select(y => y.SceneAsset)));
            var sceneAssets = AssetDatabaseUtility.FindScenes();

            _otherScenes = sceneAssets.Except(set);
        }

        private void RefreshAssetList()
        {
            _assets = AssetDatabaseUtility.FindAssetsByType<SceneLibrary>(asset => asset.Scenes != default).ToList();
            _assets.Sort(AssetsComparer);
        }

        private static int AssetsComparer(SceneLibrary a, SceneLibrary b)
        {
            var orderCompareResult = a.SortingOrder.CompareTo(b.SortingOrder);
            return orderCompareResult == default ? string.Compare(a.Title, b.Title, StringComparison.Ordinal) : orderCompareResult;
        }

        private void OnGUI()
        {
            GUI.enabled = IsEditorFree;

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            {
                if (_assets.IsNullOrEmpty())
                {
                    EditorGUILayout.LabelField("There are no hub libraries in the project.");
                }
                else
                {
                    foreach (var asset in _assets)
                    {
                        DrawAssetMenu(asset);
                        EditorGUILayout.Space();
                    }
                }

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    EditorGUILayout.LabelField("Other scenes");
                    EditorGUILayout.Space();

                    foreach (var sceneAsset in _otherScenes)
                    {
                        if (sceneAsset)
                        {
                            DrawSceneAssetMenu(sceneAsset, sceneAsset.name);
                        }
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            DrawHubMenu();
        }

        private void DrawHubMenu()
        {
            if (GUILayout.Button("Refresh"))
            {
                RefreshAssetList();
                RefreshNonAssetSceneList();
            }
        }

        private void DrawAssetMenu(SceneLibrary asset)
        {
            if (!asset) return;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                if (GUILayout.Button(asset.GetLibraryDisplayName(), EditorStyles.boldLabel)) EditorGUIUtility.PingObject(asset);

                if (asset.Scenes.IsNullOrEmpty())
                {
                    EditorGUILayout.LabelField("The collection of scenes is empty.");
                    EditorGUILayout.EndVertical();
                    return;
                }

                EditorGUILayout.Space();

                foreach (var info in asset.Scenes.Where(info => info != default && info.SceneAsset))
                {
                    DrawSceneAssetMenu(info.SceneAsset, info.GetSceneInfoDisplayName());
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawSceneAssetMenu(SceneAsset scene, string displayName)
        {
            if (!scene) return;

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button(displayName))
                {
                    Change(scene, false);
                }

                var c = GUI.color;
                GUI.color = Color.yellow;
                {
                    if (GUILayout.Button(BUILD_SCENE_ASSET_CONTENT, GUILayout.Width(18f)))
                    {
                        ToggleBuildStatus(scene);
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
                }
                GUI.color = c;
            }
            EditorGUILayout.EndHorizontal();
        }

        private static void ToggleBuildStatus(SceneAsset scene)
        {
            if (!scene) return;
            var path = AssetDatabase.GetAssetPath(scene);

            if (EditorBuildSettings.scenes.Select(x => x.path).Contains(path))
            {
                EditorBuildSettings.scenes = EditorBuildSettings.scenes.Where(x => x.path != path).ToArray();
            }
            else
            {
                var editorBuildScene = new EditorBuildSettingsScene(path, true);
                EditorBuildSettings.scenes = EditorBuildSettings.scenes.Append(editorBuildScene).ToArray();
            }

            AssetDatabase.SaveAssets();
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
