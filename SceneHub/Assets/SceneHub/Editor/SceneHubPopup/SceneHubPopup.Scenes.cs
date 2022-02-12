using System.Collections.Generic;
using System.Linq;
using SceneHub.Utilities;
using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup
    {
        private List<SceneAsset> _scenes = new List<SceneAsset>();

        private void RefreshScenes()
        {
            _scenes = AssetDatabaseUtility.FindScenes().ToList();
        }

        private void DrawScenes()
        {
            if (_scenes.IsNullOrEmpty())
            {
                EditorGUILayout.LabelField("No scenes in project ...");
            }
            else
            {
                foreach (var scene in _scenes)
                {
                    DrawSceneAssetMenu(scene, scene.name, Color.white);
                }
            }
        }
    }
}
