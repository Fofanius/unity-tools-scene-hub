using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SceneHub
{
    [CreateAssetMenu(menuName = "Scene Hub/Scene Library", order = int.MaxValue)]
    public sealed class SceneLibraryAsset : ScriptableObject
    {
#pragma warning disable 0649
        [Tooltip("Library custom name.")]
        [SerializeField] private string _title;
        [Tooltip("Display order in the hub-popup.")]
        [SerializeField] private int _sortingOrder;
        [Tooltip("Library scenes collection.")]
        [SerializeField] private List<SceneReferenceAsset> _scenes;
#pragma warning restore 0649

        /// <summary>
        /// Library custom title.
        /// </summary>
        public string Title => _title;

        /// <summary>
        /// Library display sorting order.
        /// </summary>
        public int SortingOrder => _sortingOrder;

        public bool IsValid() => Scenes.Count > 0 && Scenes.All(x => x && x.IsValid);
        
        /// <summary>
        /// Library scenes.
        /// </summary>
        public IReadOnlyList<SceneReferenceAsset> Scenes => _scenes ??= new List<SceneReferenceAsset>();
    }
}
