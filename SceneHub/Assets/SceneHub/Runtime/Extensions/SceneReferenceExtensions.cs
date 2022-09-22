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
            if (!sceneReference.IsValid) throw new ArgumentException($"Unable to load scene by reference. Reference is invalid. ({sceneReference}).", nameof(sceneReference));

            return SceneManager.LoadSceneAsync(sceneReference.ScenePath, mode);
        }

        public static bool IsLoadedAsMain(this ISceneReference sceneReference)
        {
            if (sceneReference == null) throw new ArgumentNullException(nameof(sceneReference));
            if (!sceneReference.IsValid) throw new ArgumentException($"Unable to check if scene is loaded. Reference is invalid. ({sceneReference}).", nameof(sceneReference));

            var mainScene = SceneManager.GetActiveScene();
            return IsReferenceOfScene(sceneReference, mainScene);
        }

        public static bool IsLoaded(this ISceneReference sceneReference)
        {
            if (sceneReference == null) throw new ArgumentNullException(nameof(sceneReference));
            if (!sceneReference.IsValid) throw new ArgumentException($"Unable to check if scene is loaded. Reference is invalid. ({sceneReference}).", nameof(sceneReference));

            var count = SceneManager.sceneCount;

            for (var i = 0; i < count; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (IsReferenceOfScene(sceneReference, scene))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsReferenceOfScene(ISceneReference referenceToCheck, Scene scene)
        {
            return referenceToCheck.ScenePath == scene.path;
        }
    }
}
