using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneHub
{
    public static class Extensions
    {
        /// <summary>
        /// Shortcut to <see cref="SceneManager.LoadScene(string,UnityEngine.SceneManagement.LoadSceneMode)"/>
        /// </summary>
        public static void LoadSync(this SceneInfo sceneInfo, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (sceneInfo == null) throw new ArgumentNullException(nameof(sceneInfo));
            if (string.IsNullOrWhiteSpace(sceneInfo.SceneName)) throw new ArgumentException($"Probably scene asset reference is empty ({sceneInfo}).", nameof(sceneInfo));

            SceneManager.LoadScene(sceneInfo.SceneName, mode);
        }

        /// <summary>
        /// Shortcut to <see cref="SceneManager.LoadSceneAsync(string,UnityEngine.SceneManagement.LoadSceneMode)"/>
        /// </summary>
        public static AsyncOperation LoadAsync(this SceneInfo sceneInfo, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (sceneInfo == null) throw new ArgumentNullException(nameof(sceneInfo));
            if (string.IsNullOrWhiteSpace(sceneInfo.SceneName)) throw new ArgumentException($"Probably scene asset reference is empty ({sceneInfo}).", nameof(sceneInfo));

            return SceneManager.LoadSceneAsync(sceneInfo.SceneName, mode);
        }

        /// <summary>
        /// Loading selected scene by index in <see cref="LoadSceneMode.Single"/> mode and other scenes in <see cref="LoadSceneMode.Additive"/> mode.
        /// </summary>
        /// <param name="library">Target scene library.</param>
        /// <param name="mainSceneIndex">Index of scene to <see cref="LoadSceneMode.Single"/> mode loading.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void LoadLibrary(this SceneLibrary library, int mainSceneIndex = 0, Action loadEndCallback = default)
        {
            if (!library) throw new ArgumentNullException(nameof(library));
            if (library.Scenes.Count == 0) throw new ArgumentException($"Scenes library is empty");
            if (mainSceneIndex < 0 || mainSceneIndex >= library.Scenes.Count) throw new IndexOutOfRangeException($"Main scene index \'{mainSceneIndex}\' out of range {library.Scenes.Count}");

            SceneManager.LoadScene(library.Scenes[mainSceneIndex].SceneName, LoadSceneMode.Single);

            for (var i = 0; i < library.Scenes.Count; i++)
            {
                if (i == mainSceneIndex) continue;
                SceneManager.LoadScene(library.Scenes[i].SceneName, LoadSceneMode.Additive);
            }

            loadEndCallback?.Invoke();
        }
    }
}
