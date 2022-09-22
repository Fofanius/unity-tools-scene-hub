using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace SceneHub
{
    public static class SceneLibraryExtensions
    {
        /// <summary>
        /// Loading selected scene by index in <see cref="LoadSceneMode.Single"/> mode and other scenes in <see cref="LoadSceneMode.Additive"/> mode.
        /// </summary>
        /// <param name="libraryAsset">Target scene library.</param>
        /// <param name="mainSceneIndex">Index of scene to <see cref="LoadSceneMode.Single"/> mode loading.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void LoadLibrary(this SceneLibraryAsset libraryAsset, int mainSceneIndex = 0, Action loadEndCallback = default)
        {
            if (!libraryAsset) throw new ArgumentNullException(nameof(libraryAsset));
            if (libraryAsset.Scenes.Count == 0) throw new ArgumentException($"Scene library is empty.");
            if (mainSceneIndex < 0 || mainSceneIndex >= libraryAsset.Scenes.Count) throw new IndexOutOfRangeException($"Main scene index \'{mainSceneIndex}\' out of range {libraryAsset.Scenes.Count}");

            SceneManager.LoadScene(libraryAsset.Scenes[mainSceneIndex].ScenePath, LoadSceneMode.Single);

            for (var i = 0; i < libraryAsset.Scenes.Count; i++)
            {
                if (i == mainSceneIndex) continue;
                SceneManager.LoadScene(libraryAsset.Scenes[i].ScenePath, LoadSceneMode.Additive);
            }

            loadEndCallback?.Invoke();
        }
        
        public static IEnumerator LoadLibraryAsync(this SceneLibraryAsset libraryAsset, int mainSceneIndex = 0, Action loadEndCallback = default)
        {
            if (!libraryAsset) throw new ArgumentNullException(nameof(libraryAsset));
            if (libraryAsset.Scenes.Count == 0) throw new ArgumentException($"Scene library is empty.");
            if (mainSceneIndex < 0 || mainSceneIndex >= libraryAsset.Scenes.Count) throw new IndexOutOfRangeException($"Main scene index \'{mainSceneIndex}\' out of range {libraryAsset.Scenes.Count}");

            yield return SceneManager.LoadSceneAsync(libraryAsset.Scenes[mainSceneIndex].ScenePath, LoadSceneMode.Single);

            for (var i = 0; i < libraryAsset.Scenes.Count; i++)
            {
                if (i == mainSceneIndex) continue;
                yield return  SceneManager.LoadSceneAsync(libraryAsset.Scenes[i].ScenePath, LoadSceneMode.Additive);
            }

            loadEndCallback?.Invoke();
        }
    }
}
