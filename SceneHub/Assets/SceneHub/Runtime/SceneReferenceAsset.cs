using UnityEngine;
using Random = UnityEngine.Random;

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
        [SerializeField] private Color _displayColor;

        public bool IsValid => !string.IsNullOrWhiteSpace(_scenePath);
        public string ScenePath => _scenePath;
        public Color DisplayColor => _displayColor;

#if UNITY_EDITOR
        private void Reset()
        {
            _displayColor = Random.ColorHSV(0f, 1f, .2f, .5f, 0.7f, 1f);
        }

        private void OnValidate()
        {
            _scenePath = _sceneAsset ? UnityEditor.AssetDatabase.GetAssetPath(_sceneAsset) : string.Empty;
        }
#endif

        public override string ToString() => $"{nameof(SceneReferenceAsset)} ({_scenePath})";
    }
}
