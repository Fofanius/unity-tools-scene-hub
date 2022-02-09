using UnityEngine;

namespace SceneHub
{
    [CreateAssetMenu(fileName = "Scene Reference", menuName = "Scene Hub/Scene Reference", order = int.MaxValue)]
    public sealed class SceneReferenceAsset : ScriptableObject, ISceneReference
    {
#if UNITY_EDITOR
        [SerializeField] private UnityEditor.SceneAsset _sceneAsset;
#endif
        [HideInInspector]
        [SerializeField] private string _scenePath;

        public bool IsValid => !string.IsNullOrWhiteSpace(_scenePath);
        public string ScenePath => _scenePath;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _scenePath = _sceneAsset ? UnityEditor.AssetDatabase.GetAssetPath(_sceneAsset) : string.Empty;
        }
#endif

        public override string ToString() => $"{nameof(SceneReferenceAsset)} ({_scenePath})";
    }
}
