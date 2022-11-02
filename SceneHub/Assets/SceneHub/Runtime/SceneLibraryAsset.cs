using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SceneHub
{
    [CreateAssetMenu(menuName = "Scene Hub/Scene Library")]
    public sealed class SceneLibraryAsset : ScriptableObject
    {
        [Tooltip("Library scenes collection.")]
        [SerializeField] private List<SceneReferenceAsset> _sceneReferences;

        public bool IsValid() => SceneReferences.Count(x => !x.IsNullOrInvalid()) > 0;

        /// <summary>
        /// Library scenes.
        /// </summary>
        public IReadOnlyList<SceneReferenceAsset> SceneReferences => _sceneReferences ??= new List<SceneReferenceAsset>();

        public IEnumerable<string> GetValidScenes()
        {
            return SceneReferences.Where(x => !x.IsNullOrInvalid()).Select(x => x.ScenePath);
        }
    }
}
