using UnityEditor;
using UnityEngine;

namespace SceneHub
{
    [System.Serializable]
    public sealed class SceneInfo
    {
#pragma warning disable 0649
        [Tooltip("Custom title. It will show name of scene by default.")]
        [SerializeField] internal string Title;
        [Tooltip("Scene asset.")]
        [SerializeField] internal SceneAsset Scene;
#pragma warning restore 0649

        public string SafeTitle => string.IsNullOrWhiteSpace(Title) ? $"[{(Scene ? Scene.name : "Empty")}]" : Title;

        public override string ToString() => $"{Title} [{Scene}]";
    }
}