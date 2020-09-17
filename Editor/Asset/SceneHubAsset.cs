using System.Collections.Generic;
using UnityEngine;

namespace SceneHub
{
    [CreateAssetMenu(fileName = "SceneHub", menuName = "Scene Hub/Scene Library", order = int.MaxValue)]
    public sealed class SceneHubAsset : ScriptableObject
    {
#pragma warning disable 0649
        [Tooltip("Library custom name.")]
        [SerializeField] internal string Title;
        [Tooltip("Display order in the hub-popup.")]
        [SerializeField] internal int Order;
        [Tooltip("Library scenes collection.")]
        [SerializeField] internal List<SceneInfo> Scenes;
#pragma warning restore 0649

        /// <summary>
        /// Вернет либо <see cref="Title"/>, или '[*название ассета*]', если пользовательский заголовок пуст. 
        /// </summary>
        public string SafeTitle => string.IsNullOrWhiteSpace(Title) ? $"[{name}]" : Title;
    }
}