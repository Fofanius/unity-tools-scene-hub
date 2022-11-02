using SceneHub.Editor.Utilities;
using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup
    {
        private GenericMenu GetSceneMenu(SceneAsset scene)
        {
            var menu = new GenericMenu();

            menu.AddItem(SceneHubGUIContent.SceneMenu.PingSceneAsset, false, () => EditorGUIUtility.PingObject(scene));

            BuildContextMenuForFavoriteScene(menu, scene);
            BuildContextMenuForBuildScene(menu, scene);

            return menu;
        }

        private void BuildContextMenuForBuildScene(GenericMenu menu, SceneAsset scene)
        {
            var isInBuildList = SceneManagementUtility.IsBuildScene(scene);
            if (isInBuildList)
            {
                menu.AddItem(SceneHubGUIContent.BuildListMenu.RemoveFromBuildList, false, () => SceneManagementUtility.RemoveFromBuildList(scene));

                var enabledInBuildList = SceneManagementUtility.IsEnabledInBuildList(scene);
                if (enabledInBuildList)
                {
                    menu.AddItem(SceneHubGUIContent.BuildListMenu.DisableInBuildList, false, () => SceneManagementUtility.SetEnabledInBuildList(scene, false));
                }
                else
                {
                    menu.AddItem(SceneHubGUIContent.BuildListMenu.EnableInBuildList, false, () => SceneManagementUtility.SetEnabledInBuildList(scene, true));
                }
            }
            else
            {
                menu.AddItem(SceneHubGUIContent.BuildListMenu.AddToBuildList, false, () => SceneManagementUtility.AddToBuildList(scene));
            }
        }
    }
}
