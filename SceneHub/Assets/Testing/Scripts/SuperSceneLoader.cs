using SceneHub;
using UnityEngine;

public class SuperSceneLoader : MonoBehaviour
{
    [SerializeField] private SceneLibraryAsset _libraryAsset;

    public void OnClick()
    {
        _libraryAsset.LoadLibrary(default, OnLibraryLoaded);
    }

    private void OnLibraryLoaded()
    {
        Debug.Log("Library is loaded", _libraryAsset);
    }
}
