using System;
using System.Collections;
using System.Linq;
using UnityEngine.SceneManagement;

namespace SceneHub
{
    public static class SceneLibraryExtensions
    {
        private static void ValidateSceneLibrary(SceneLibraryAsset libraryAsset, int mainSceneIndex)
        {
            if (!libraryAsset)
            {
                throw new ArgumentNullException(nameof(libraryAsset));
            }

            if (libraryAsset.SceneReferences.Any(x => x.IsNullOrInvalid()))
            {
                throw new ArgumentException($"Scene library {libraryAsset} contains invalid references!");
            }

            if (mainSceneIndex < 0 || mainSceneIndex >= libraryAsset.SceneReferences.Count)
            {
                throw new IndexOutOfRangeException($"Main scene index \'{mainSceneIndex}\' out of range {libraryAsset.SceneReferences.Count}");
            }
        }

        /// <summary>
        /// Loading selected scene by index in <see cref="LoadSceneMode.Single"/> mode and other scenes in <see cref="LoadSceneMode.Additive"/> mode.
        /// </summary>
        /// <param name="libraryAsset">Target scene library.</param>
        /// <param name="mainSceneIndex">Index of scene to <see cref="LoadSceneMode.Single"/> mode loading.</param>
        /// <param name="loadEndCallback">Executes at the end of <see cref="SceneManager.LoadScene(string,UnityEngine.SceneManagement.LoadSceneMode)"/> loop.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void LoadLibrary(this SceneLibraryAsset libraryAsset, int mainSceneIndex = 0, Action loadEndCallback = default)
        {
            ValidateSceneLibrary(libraryAsset, mainSceneIndex);

            libraryAsset.SceneReferences[mainSceneIndex].Load(LoadSceneMode.Single);

            for (var i = 0; i < libraryAsset.SceneReferences.Count; i++)
            {
                if (i == mainSceneIndex) continue;
                libraryAsset.SceneReferences[i].Load(LoadSceneMode.Additive);
            }

            loadEndCallback?.Invoke();
        }

        /// <summary>
        /// Loading selected scene by index in <see cref="LoadSceneMode.Single"/> mode and other scenes in <see cref="LoadSceneMode.Additive"/> mode.
        /// </summary>
        /// <param name="libraryAsset">Target scene library.</param>
        /// <param name="mainSceneIndex">Index of scene to <see cref="LoadSceneMode.Single"/> mode loading.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static IEnumerator LoadLibraryAsync(this SceneLibraryAsset libraryAsset, int mainSceneIndex = 0)
        {
            ValidateSceneLibrary(libraryAsset, mainSceneIndex);

            yield return libraryAsset.SceneReferences[mainSceneIndex].LoadAsync(LoadSceneMode.Single);

            for (var i = 0; i < libraryAsset.SceneReferences.Count; i++)
            {
                if (i == mainSceneIndex) continue;
                yield return libraryAsset.SceneReferences[i].LoadAsync(LoadSceneMode.Additive);
            }
        }

        public static bool IsNullOrInvalid(this SceneLibraryAsset libraryAsset) => !libraryAsset || !libraryAsset.IsValid();
    }
}
