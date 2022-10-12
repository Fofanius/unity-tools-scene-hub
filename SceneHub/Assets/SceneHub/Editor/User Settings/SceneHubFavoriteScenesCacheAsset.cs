using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor.UserSettings
{
    [FilePath("UserSettings/SceneHub/FavoriteScenes.asset", FilePathAttribute.Location.ProjectFolder)]
    internal class SceneHubFavoriteScenesCacheAsset : ScriptableSingleton<SceneHubFavoriteScenesCacheAsset>, IReadOnlyList<SceneAsset>
    {
        [SerializeField] private List<SceneAsset> _favoriteScenes;

        internal List<SceneAsset> EntitiesInternal => _favoriteScenes ??= new List<SceneAsset>();

        public SceneAsset this[int index] => EntitiesInternal[index];

        public int Count => EntitiesInternal.Count;

        private void OnValidate()
        {
            EntitiesInternal.RemoveAll(x => !x);
        }

        internal bool IsFavorite(SceneAsset scene) => EntitiesInternal.Contains(scene);

        internal void AddToFavorite(SceneAsset scene)
        {
            if (IsFavorite(scene)) return;
            EntitiesInternal.Add(scene);
        }

        internal void RemoveFromFavorite(SceneAsset sceneAsset)
        {
            EntitiesInternal.RemoveAll(x => x == sceneAsset);
        }

        internal void MoveUp(SceneAsset scene)
        {
            var index = EntitiesInternal.IndexOf(scene);
            if (index > 0)
            {
                Swap(index, index - 1);
            }
        }

        internal void MoveDown(SceneAsset scene)
        {
            var index = EntitiesInternal.IndexOf(scene);
            if (index != -1 && index < EntitiesInternal.Count - 1)
            {
                Swap(index, index + 1);
            }
        }

        internal void SetFirst(SceneAsset scene)
        {
            var index = EntitiesInternal.IndexOf(scene);
            if (index == -1) return;

            EntitiesInternal.RemoveAt(index);
            EntitiesInternal.Insert(0, scene);
        }

        internal void SetLast(SceneAsset scene)
        {
            var index = EntitiesInternal.IndexOf(scene);
            if (index == -1) return;

            EntitiesInternal.RemoveAt(index);
            EntitiesInternal.Add(scene);
        }

        private void Swap(int indexA, int indexB)
        {
            var temp = EntitiesInternal[indexA];
            EntitiesInternal[indexA] = EntitiesInternal[indexB];
            EntitiesInternal[indexB] = temp;
        }

        #region IEnumerator

        public IEnumerator<SceneAsset> GetEnumerator()
        {
            return EntitiesInternal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
