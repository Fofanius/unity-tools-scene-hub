using System.Collections.Generic;
using SceneHub.Editor.UserSettings;
using SceneHub.Utilities;
using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup
    {
        private List<SceneAsset> FavoriteScenes => SceneHubFavoriteScenesCacheAsset.instance.FavoriteScenes;

        private bool IsFavorite(SceneAsset scene) => SceneHubFavoriteScenesCacheAsset.instance.IsFavorite(scene);

        private void AddToFavorite(SceneAsset scene) => SceneHubFavoriteScenesCacheAsset.instance.AddToFavorite(scene);

        private void RemoveFromFavorite(SceneAsset scene) => SceneHubFavoriteScenesCacheAsset.instance.RemoveFromFavorite(scene);

        private void DrawFavoriteScenes()
        {
            var favorites = FavoriteScenes;
            if (favorites.IsNullOrEmpty())
            {
                EditorGUILayout.LabelField("There are no favorite scenes yet . . .");
            }
            else
            {
                for (var i = 0; i < favorites.Count; i++)
                {
                    var favorite = favorites[i];
                    DrawSceneAssetMenu(favorite, favorite.name, Color.white);
                }
            }
        }
    }
}
