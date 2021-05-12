using UnityEngine;

namespace SceneHub
{
    [System.Serializable]
    public sealed class SceneInfo
    {
#pragma warning disable 0649
        [Tooltip("Custom title")]
        [SerializeField] private string _title;
        [HideInInspector]
        [SerializeField] private string _sceneName;
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
        public string SceneName => _sceneName;
#if UNITY_EDITOR
        /// <summary>
        /// Exists only when <b>UNITY_EDITOR</b> defined.
        /// </summary>
        public UnityEditor.SceneAsset SceneAsset => _sceneAsset;

        public void Validate()
        {
            _sceneName = _sceneAsset ? _sceneAsset.name : string.Empty;
        }
#endif

        public override string ToString() => $"{_title} ({_sceneName})";
    }
}
