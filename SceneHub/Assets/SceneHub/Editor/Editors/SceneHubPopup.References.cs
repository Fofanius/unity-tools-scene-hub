using System.Collections.Generic;
using SceneHub.Utilities;
using UnityEditor;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup
    {
        private List<SceneReferenceAsset> _references = new List<SceneReferenceAsset>();

        private void RefreshReferences()
        {
            _references = AssetDatabaseUtility.FindAssetsByType<SceneReferenceAsset>();
        }

        private void DrawReferences()
        {
            if (_references.IsNullOrEmpty())
            {
                EditorGUILayout.LabelField("There is no references in projects . . .");
            }
            else
            {
                foreach (var reference in _references)
                {
                    DrawSceneReferenceMenu(reference, reference.name);
                }
            }
        }
    }
}
