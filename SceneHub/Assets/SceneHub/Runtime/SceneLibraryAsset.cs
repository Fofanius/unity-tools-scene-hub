using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SceneHub
{
    [CreateAssetMenu(menuName = "Scene Hub/Scene Library")]
    public sealed class SceneLibraryAsset : ScriptableObject
    {
#pragma warning disable 0649
        [Tooltip("Library custom name.")]
        [SerializeField] private string _title;
        [Tooltip("Display order in the hub-popup.")]
        [SerializeField] private int _sortingOrder;
        [Tooltip("Library scenes collection.")]
        [SerializeField] private List<SceneReferenceAsset> _sceneReferences;
#pragma warning restore 0649

        /// <summary>
        /// Library custom title.
        /// </summary>
        public string Title => _title;

        /// <summary>
        /// Library display sorting order.
        /// </summary>
        public int SortingOrder => _sortingOrder;

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
