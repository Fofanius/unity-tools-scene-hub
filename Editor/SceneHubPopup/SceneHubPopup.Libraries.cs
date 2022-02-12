using System;
using System.Collections.Generic;
using System.Linq;
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
            _assets = AssetDatabaseUtility.FindAssetsByType<SceneLibraryAsset>(asset => asset.Scenes != default).ToList();
            _assets.Sort(AssetsComparer);
        }

        private static int AssetsComparer(SceneLibraryAsset a, SceneLibraryAsset b)
        {
            var orderCompareResult = a.SortingOrder.CompareTo(b.SortingOrder);
            return orderCompareResult == default ? string.Compare(a.Title, b.Title, StringComparison.Ordinal) : orderCompareResult;
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
                if (GUILayout.Button(asset.GetLibraryDisplayName(), EditorStyles.boldLabel)) EditorGUIUtility.PingObject(asset);

                if (asset.Scenes.IsNullOrEmpty())
                {
                    EditorGUILayout.LabelField("The collection of scenes is empty.");
                }
                else
                {
                    EditorGUILayout.Space();

                    foreach (var info in asset.Scenes)
                    {
                        DrawSceneReferenceMenu(info, info.GetSceneInfoDisplayName(), Color.white);
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }
    }
}
