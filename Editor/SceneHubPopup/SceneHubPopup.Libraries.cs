using System;
using System.Collections.Generic;
using System.Linq;
using SceneHub.Editor.Utilities;
using SceneHub.Utilities;
using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup
    {
        private List<SceneLibraryAsset> _assets;

        private void RefreshLibraries()
        {
            _assets = AssetDatabaseUtility.FindAssetsByType<SceneLibraryAsset>(asset => asset.SceneReferences != default).ToList();
        }

        private void DrawLibraries()
        {
            if (_assets.IsNullOrEmpty())
            {
                EditorGUILayout.LabelField("There are no hub libraries in the project . . .");
            }
            else
            {
                foreach (var asset in _assets)
                {
                    DrawLibraryAsset(asset);
                    EditorGUILayout.Space();
                }
            }
        }

        private void DrawLibraryAsset(SceneLibraryAsset asset)
        {
            if (!asset) return;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            {
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button(asset.name, EditorStyles.boldLabel))
                    {
                        EditorGUIUtility.PingObject(asset);
                    }

                    var c = GUI.color;
                    GUI.color = new Color(.6f, .77f, .92f);
                    {
                        if (GUILayout.Button(SceneHubGUIContent.SceneLibraryOptions.LoadAllScenes, GUILayout.Width(24f)))
                        {
                            Change(asset);
                        }
                    }
                    GUI.color = c;
                }
                EditorGUILayout.EndHorizontal();

                if (asset.SceneReferences.IsNullOrEmpty())
                {
                    EditorGUILayout.LabelField("The collection of scenes is empty.");
                }
                else
                {
                    EditorGUILayout.Space();

                    foreach (var info in asset.SceneReferences)
                    {
                        DrawSceneReferenceMenu(info, info ? info.SceneName : "NULL");
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void Change(SceneLibraryAsset libraryAsset)
        {
            SceneManagementUtility.LoadAll(libraryAsset);
            Close();
        }
    }
}
