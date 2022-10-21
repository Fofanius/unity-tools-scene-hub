using SceneHub.Editor.UserSettings;
using SceneHub.Utilities;
using UnityEditor;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup
    {
        private SceneHubFavoriteScenesSettingsAsset FavoriteScenesSettings => SceneHubFavoriteScenesSettingsAsset.instance;

        private void DrawFavoriteScenes()
        {
            if (FavoriteScenesSettings.IsNullOrEmpty())
            {
                EditorGUILayout.LabelField("There are no favorite scenes yet . . .");
            }
            else
            {
                for (var i = 0; i < FavoriteScenesSettings.Count; i++)
                {
                    var favorite = FavoriteScenesSettings[i];
                    DrawSceneAssetMenu(favorite, favorite.name, AssetDatabase.GetAssetPath(favorite));
                }
            }
        }

        private void BuildContextMenuForFavoriteScene(GenericMenu menu, SceneAsset scene)
        {
            var isFavorite = FavoriteScenesSettings.IsFavorite(scene);
            if (isFavorite)
            {
                menu.AddItem(SceneHubGUIContent.FavoriteMenu.RemoveFromFavorites, false, () => FavoriteScenesSettings.RemoveFromFavorite(scene));
                menu.AddSeparator(SceneHubGUIContent.FavoriteMenu.FAVORITE_MENU_CATEGORY_WITH_MENU_SEPARATOR);
                menu.AddItem(SceneHubGUIContent.FavoriteMenu.MoveUpInFavoriteList, false, () => FavoriteScenesSettings.MoveUp(scene));
                menu.AddItem(SceneHubGUIContent.FavoriteMenu.MoveDownInFavoriteList, false, () => FavoriteScenesSettings.MoveDown(scene));
                menu.AddSeparator(SceneHubGUIContent.FavoriteMenu.FAVORITE_MENU_CATEGORY_WITH_MENU_SEPARATOR);
                menu.AddItem(SceneHubGUIContent.FavoriteMenu.SetAsFirstInFavorites, false, () => FavoriteScenesSettings.SetFirst(scene));
                menu.AddItem(SceneHubGUIContent.FavoriteMenu.SetAsLastInFavorites, false, () => FavoriteScenesSettings.SetLast(scene));
            }
            else
            {
                menu.AddItem(SceneHubGUIContent.FavoriteMenu.AddToFavorites, false, () => FavoriteScenesSettings.AddToFavorite(scene));
            }
        }
    }
}
