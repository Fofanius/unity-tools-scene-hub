using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SceneHub.UI
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneByReferenceButtonAddon : MonoBehaviour
    {
        [SerializeField] private SceneReferenceAsset _sceneReference;
        [Space]
        [SerializeField] private LoadSceneMode _loadMode;

#if UNITY_EDITOR
        private void Reset()
        {
            var button = GetComponent<Button>();

            UnityEditor.Events.UnityEventTools.AddPersistentListener(button.onClick, LoadScene);
            UnityEditor.EditorUtility.SetDirty(button);
        }
#endif

        public void LoadScene()
        {
            if (_sceneReference && _sceneReference.IsValid)
            {
                _sceneReference.LoadSync(_loadMode);
            }
            else
            {
                Debug.LogError($"Unable to load scene. Reference is empty", this);
            }
        }
    }
}
