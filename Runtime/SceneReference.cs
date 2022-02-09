using UnityEngine;

namespace SceneHub
{
    [System.Serializable]
    public sealed class SceneReference : ISceneReference
    {
#pragma warning disable 0649
        [Tooltip("Custom title")]
        [SerializeField] private string _title;
        [HideInInspector]
        [SerializeField] private string _scenePath;
#if UNITY_EDITOR
        [Tooltip("Scene asset.")]
        [SerializeField] private UnityEditor.SceneAsset _sceneAsset;
#endif
#pragma warning restore 0649

        /// <summary>
        /// Custom user title.
        /// </summary>
        public string Title => _title;

        /// <summary>
        /// Scene asset name.
        /// </summary>
        public string ScenePath => _scenePath;

        public bool IsValid => !string.IsNullOrWhiteSpace(_scenePath);
#if UNITY_EDITOR
        /// <summary>
        /// Exists only when <b>UNITY_EDITOR</b> defined.
        /// </summary>
        public UnityEditor.SceneAsset SceneAsset => _sceneAsset;

        /// <summary>
        /// Exists only when <b>UNITY_EDITOR</b> defined.
        /// </summary>
        internal void OnValidate()
        {
            _scenePath = _sceneAsset ? UnityEditor.AssetDatabase.GetAssetPath(_sceneAsset) : string.Empty;
        }
#endif

        public override string ToString() => $"{_title} ({_scenePath})";
    }
}
