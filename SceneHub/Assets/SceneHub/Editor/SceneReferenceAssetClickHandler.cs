using SceneHub.Editor.Utilities;

namespace SceneHub.Editor
{
    internal static class SceneReferenceAssetClickHandler
    {
        [UnityEditor.Callbacks.OnOpenAsset(2)]
        private static bool OnAssetClick(int instanceID, int line)
        {
            var assetPath = UnityEditor.AssetDatabase.GetAssetPath(instanceID);
            var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<SceneReferenceAsset>(assetPath);

            if (asset)
            {
                SceneManagementUtility.ChangeScene(asset.ScenePath);
            }

            return false;
        }
    }
}
