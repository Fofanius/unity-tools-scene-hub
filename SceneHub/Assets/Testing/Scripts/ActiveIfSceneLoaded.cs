using SceneHub;
using UnityEngine;

public class ActiveIfSceneLoaded : MonoBehaviour
{
    [SerializeField] private SceneReferenceAsset _sceneReferenceAsset;

    private void Start()
    {
        gameObject.SetActive(_sceneReferenceAsset && _sceneReferenceAsset.IsLoaded());
    }
}
