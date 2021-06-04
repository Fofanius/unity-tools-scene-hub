using SceneHub;
using UnityEngine;

public class SuperSceneLoader : MonoBehaviour
{
    [SerializeField] private SceneLibrary _library;

    public void OnClick()
    {
        _library.LoadLibrary(default, OnLibraryLoaded);
    }

    private void OnLibraryLoaded()
    {
        Debug.Log("Library is loaded", _library);
    }
}
