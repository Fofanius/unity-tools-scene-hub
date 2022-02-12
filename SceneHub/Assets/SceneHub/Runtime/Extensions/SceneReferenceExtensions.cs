using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneHub
{
    public static class SceneReferenceExtensions
    {
        /// <summary>
        /// Shortcut to <see cref="SceneManager.LoadScene(string,UnityEngine.SceneManagement.LoadSceneMode)"/>
        /// </summary>
        public static void LoadSync(this ISceneReference sceneReference, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (sceneReference == null) throw new ArgumentNullException(nameof(sceneReference));

            if (sceneReference.IsValid)
            {
                SceneManager.LoadScene(sceneReference.ScenePath, mode);
            }
            else
            {
                throw new ArgumentException($"Unable to load scene by reference. Reference is invalid. ({sceneReference}).", nameof(sceneReference));
            }
        }

        /// <summary>
        /// Shortcut to <see cref="SceneManager.LoadSceneAsync(string,UnityEngine.SceneManagement.LoadSceneMode)"/>
        /// </summary>
        public static AsyncOperation LoadAsync(this ISceneReference sceneReference, LoadSceneMode mode = LoadSceneMode.Single)
        {
            if (sceneReference == null) throw new ArgumentNullException(nameof(sceneReference));
            if (!sceneReference.IsValid) throw new ArgumentException($"Probably scene asset reference is empty ({sceneReference}).", nameof(sceneReference));

            return SceneManager.LoadSceneAsync(sceneReference.ScenePath, mode);
        }
    }
}
