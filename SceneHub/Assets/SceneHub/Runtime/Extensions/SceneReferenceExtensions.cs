using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneHub
{
    public static class SceneReferenceExtensions
    {
        private static void ValidateSceneReference(ISceneReference sceneReference)
        {
            if (sceneReference == null)
            {
                throw new ArgumentNullException(nameof(sceneReference));
            }

            if (!sceneReference.IsValid)
            {
                throw new ArgumentException($"Unable to load scene by reference. Reference is invalid. ({sceneReference}).", nameof(sceneReference));
            }
        }

        /// <summary>
        /// <inheritdoc cref="Load(SceneHub.ISceneReference, UnityEngine.SceneManagement.LoadSceneMode)"/>
        /// </summary>
        /// <param name="sceneReference"></param>
        /// <param name="mode"></param>
        [Obsolete("Obsolete naming. Use Load(ISceneReference, mode) instead!")]
        public static void LoadSync(this ISceneReference sceneReference, LoadSceneMode mode = LoadSceneMode.Single) => Load(sceneReference, mode);

        /// <summary>
        /// Shortcut to <see cref="SceneManager.LoadScene(string,UnityEngine.SceneManagement.LoadSceneMode)"/>
        /// </summary>
        public static void Load(this ISceneReference sceneReference, LoadSceneMode mode = LoadSceneMode.Single)
        {
            ValidateSceneReference(sceneReference);
            SceneManager.LoadScene(sceneReference.ScenePath, mode);
        }

        /// <summary>
        /// Shortcut to <see cref="SceneManager.LoadSceneAsync(string,UnityEngine.SceneManagement.LoadSceneMode)"/>
        /// </summary>
        public static AsyncOperation LoadAsync(this ISceneReference sceneReference, LoadSceneMode mode = LoadSceneMode.Single)
        {
            ValidateSceneReference(sceneReference);
            return SceneManager.LoadSceneAsync(sceneReference.ScenePath, mode);
        }

        public static bool IsLoadedAsMain(this ISceneReference sceneReference)
        {
            ValidateSceneReference(sceneReference);

            var mainScene = SceneManager.GetActiveScene();
            return IsReferenceOfScene(sceneReference, mainScene);
        }

        public static bool IsLoaded(this ISceneReference sceneReference)
        {
            ValidateSceneReference(sceneReference);

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
