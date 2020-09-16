using UnityEditor;
using UnityEditor.SceneManagement;

namespace SceneHub.Utilities
{
    public static class SceneManagmentUtility 
    {
        /// <summary>
        /// Сохраняет текущую активную сцену и осуществляет переход на заданную.
        /// </summary>
        public static void ChangeScene(SceneAsset scene)
        {
            if(!scene) return;
            
            // сохраняем текущую активну
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
            
            // переходим на новую
            var path = AssetDatabase.GetAssetPath(scene);
            EditorSceneManager.OpenScene(path);
        }
    }
}