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

        private List<SceneHubAsset> _assets;
        private Vector2 _scroll;

        private static bool IsEditorFree => !EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isCompiling;

        [MenuItem("Scene Hub/Move To %&q", priority = 100)]
        public static void ShowWindow()
        {
            var window = GetWindow<SceneHubPopup>();

            window.titleContent = new GUIContent("Change Scene");
            window.ShowPopup();
        }

        [MenuItem("Scene Hub/Move To %&q", true)] private static bool ShowValidate() => AssetDatabaseUtility.FindAssetsByType<SceneHubAsset>().Count > 0 && IsEditorFree;

        private void OnEnable()
        {
            _assets = AssetDatabaseUtility.FindAssetsByType<SceneHubAsset>(asset => asset.Scenes != default).ToList();
            _assets.Sort(AssetsComparer);

            minSize = new Vector2(300, 200);
        }

        private static int AssetsComparer(SceneHubAsset a, SceneHubAsset b)
        {
            var orderCompareResult = a.Order.CompareTo(b.Order);
            return orderCompareResult == default ? string.Compare(a.SafeTitle, b.SafeTitle, StringComparison.Ordinal) : orderCompareResult;
        }

        private void OnGUI()
        {
            if (_assets.IsNullOrEmpty())
            {
                EditorGUILayout.LabelField("There are no hub libraries in the project.");
                return;
            }

            GUI.enabled = IsEditorFree;

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            {
                foreach (var asset in _assets)
                {
                    DrawAssetMenu(asset);
                    EditorGUILayout.Space();
                }
            }
            EditorGUILayout.EndScrollView();
        }

        private void DrawAssetMenu(SceneHubAsset asset)
        {
            if (!asset) return;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                if (GUILayout.Button(asset.SafeTitle, EditorStyles.boldLabel)) EditorGUIUtility.PingObject(asset);

                if (asset.Scenes.IsNullOrEmpty())
                {
                    EditorGUILayout.LabelField("The collection of scenes is empty.");
                    EditorGUILayout.EndVertical();
                    return;
                }

                EditorGUILayout.Space();

                foreach (var info in asset.Scenes.Where(info => info != default && info.Scene))
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button(info.SafeTitle))
                        {
                            Change(info.Scene, false);
                        }

                        GUI.color = Color.green;
                        {
                            if (GUILayout.Button(MOVE_AND_PLAY_CONTENT, GUILayout.Width(40f)))
                            {
                                Change(asset.Scenes[0].Scene, true);
                            }
                        }
                        GUI.color = Color.white;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void Change(SceneAsset scene, bool needStartPlaymode)
        {
            SceneManagmentUtility.ChangeScene(scene);
            Close();

            if (needStartPlaymode)
            {
                EditorApplication.isPlaying = true;
            }
        }
    }
}