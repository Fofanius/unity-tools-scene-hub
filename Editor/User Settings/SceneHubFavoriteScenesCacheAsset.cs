using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor.UserSettings
{
    [FilePath("UserSettings/SceneHub/FavoriteScenes.asset", FilePathAttribute.Location.ProjectFolder)]
    public class SceneHubFavoriteScenesCacheAsset : ScriptableSingleton<SceneHubFavoriteScenesCacheAsset>
    {
        [SerializeField] private List<SceneAsset> _favoriteScenes;

        internal List<SceneAsset> FavoriteScenes => _favoriteScenes ??= new List<SceneAsset>();

        private void OnValidate()
        {
            FavoriteScenes.RemoveAll(x => !x);
        }
        
        internal bool IsFavorite(SceneAsset scene) => FavoriteScenes.Contains(scene);

        internal void AddToFavorite(SceneAsset scene)
        {
            if (IsFavorite(scene)) return;
            FavoriteScenes.Add(scene);
        }

        internal void RemoveFromFavorite(SceneAsset sceneAsset)
        {
            FavoriteScenes.RemoveAll(x => x == sceneAsset);
        }
    }
}
