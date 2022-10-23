using SceneHub.Editor.Utilities;

namespace SceneHub.Editor
{
    internal static class SceneHubAssetsClickHandler
    {
        [UnityEditor.Callbacks.OnOpenAsset(2)]
        private static bool OnSceneHubAssetClick(int instanceID, int line)
        {
            if (HandleSceneReferenceAssetClick(AssetDatabaseUtility.GetAssetByID<SceneReferenceAsset>(instanceID)))
            {
                return true;
            }

            if (HandleSceneLibraryAssetClick(AssetDatabaseUtility.GetAssetByID<SceneLibraryAsset>(instanceID)))
            {
                return true;
            }

            return false;
        }


        private static bool HandleSceneReferenceAssetClick(SceneReferenceAsset sceneReferenceAsset)
        {
            if (sceneReferenceAsset.IsNullOrInvalid()) return false;

            SceneManagementUtility.ChangeScene(sceneReferenceAsset.ScenePath);
            return true;
        }

        private static bool HandleSceneLibraryAssetClick(SceneLibraryAsset sceneLibraryAsset)
        {
            if (sceneLibraryAsset.IsNullOrInvalid()) return false;

            SceneManagementUtility.LoadAll(sceneLibraryAsset);
            return true;
        }
    }
}
