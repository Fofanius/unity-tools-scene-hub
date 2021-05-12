using UnityEngine;

namespace SceneHub.Testing
{
    public class SampleSceneLoader : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private SceneLibrary _library;
        [SerializeField] private int _index;
#pragma warning restore 0649

        public void OnLoadClick()
        {
            _library.Scenes[_index].LoadSync();
        }
    }
}
