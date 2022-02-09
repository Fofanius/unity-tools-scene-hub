using System.Collections.Generic;
using UnityEngine;

namespace SceneHub
{
    [CreateAssetMenu(fileName = "Scene Library", menuName = "Scene Hub/Scene Library", order = int.MaxValue)]
    public sealed class SceneLibraryAsset : ScriptableObject
    {
#pragma warning disable 0649
        [Tooltip("Library custom name.")]
        [SerializeField] private string _title;
        [Tooltip("Display order in the hub-popup.")]
        [SerializeField] private int _sortingOrder;
        [Tooltip("Library scenes collection.")]
        [SerializeField] private List<SceneReference> _scenes = new List<SceneReference>();
#pragma warning restore 0649

        /// <summary>
        /// Library custom title.
        /// </summary>
        public string Title => _title;

        /// <summary>
        /// Library display sorting order.
        /// </summary>
        public int SortingOrder => _sortingOrder;

        /// <summary>
        /// Library scenes.
        /// </summary>
        public IReadOnlyList<SceneReference> Scenes => _scenes;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _scenes.ForEach(x => x.OnValidate());
        }
#endif
    }
}
