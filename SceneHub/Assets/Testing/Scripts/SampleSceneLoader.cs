using UnityEngine;

namespace SceneHub.Testing
{
    public class SampleSceneLoader : MonoBehaviour
    {
#pragma warning disable 0649
        [SerializeField] private SceneLibraryAsset _libraryAsset;
        [SerializeField] private int _index;
#pragma warning restore 0649

        public void OnLoadClick()
        {
            _libraryAsset.SceneReferences[_index].Load();
        }
    }
}
