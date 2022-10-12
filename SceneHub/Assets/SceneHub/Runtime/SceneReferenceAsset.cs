using UnityEngine;

namespace SceneHub
{
    [CreateAssetMenu(fileName = "Scene Reference.asset", menuName = "Scene Hub/Scene Reference", order = int.MaxValue)]
    public sealed class SceneReferenceAsset : ScriptableObject, ISceneReference
    {
#if UNITY_EDITOR
        [SerializeField] private UnityEditor.SceneAsset _sceneAsset;
#endif
        [HideInInspector]
        [SerializeField] private string _scenePath;
        [HideInInspector]
        [SerializeField] private string _sceneName;

        public bool IsValid => !string.IsNullOrWhiteSpace(_scenePath);
        public string ScenePath => _scenePath;
        public string SceneName => _sceneName;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_sceneAsset)
            {
                _scenePath = UnityEditor.AssetDatabase.GetAssetPath(_sceneAsset);
                _sceneName = _sceneAsset.name;
            }
            else
            {
                _sceneName = _scenePath = string.Empty;
            }
        }
#endif

        public override string ToString() => $"{nameof(SceneReferenceAsset)} ({_scenePath})";
    }
}
