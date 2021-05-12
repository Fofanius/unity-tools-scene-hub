using System;
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

        public static void LoadLibrary(this SceneLibrary library, int mainSceneIndex = 0)
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
        }
    }
}
