using SceneHub.Editor.UserSettings;
using SceneHub.Utilities;
using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor
{
    public partial class SceneHubPopup
    {
        private const string FAVORITE_MENU_CATEGORY = "Favorite";

        private readonly GUIContent ADD_TO_FAVORITE_SCENE_ASSET_CONTENT = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Add", "Add to favorite list.");
        private readonly GUIContent MOVE_UP_FAVORITE_SCENE_ASSET_CONTENT = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Move Up", "Move up in favorite list.");
        private readonly GUIContent MOVE_DOWN_FAVORITE_SCENE_ASSET_CONTENT = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Move Down", "Move down in favorite list.");
        private readonly GUIContent REMOVE_FROM_FAVORITE_SCENE_ASSET_CONTENT = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Remove", "Remove from favorite list.");
        private readonly GUIContent SET_FIRST_IN_FAVORITE_SCENE_ASSET_CONTENT = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Set as First", "Move scene to the top of favorite list.");
        private readonly GUIContent SET_LAST_IN_FAVORITE_SCENE_ASSET_CONTENT = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Set as Last", "Move scene to the bottom of favorite list.");

        private SceneHubFavoriteScenesCacheAsset FavoriteScenesCache => SceneHubFavoriteScenesCacheAsset.instance;

        private void DrawFavoriteScenes()
        {
            if (FavoriteScenesCache.IsNullOrEmpty())
            {
                EditorGUILayout.LabelField("There are no favorite scenes yet . . .");
            }
            else
            {
                for (var i = 0; i < FavoriteScenesCache.Count; i++)
                {
                    var favorite = FavoriteScenesCache[i];
                    DrawSceneAssetMenu(favorite, favorite.name);
                }
            }
        }

        private void BuildContextMenuForFavoriteScene(GenericMenu menu, SceneAsset scene)
        {
            var isFavorite = FavoriteScenesCache.IsFavorite(scene);
            if (isFavorite)
            {
                menu.AddItem(REMOVE_FROM_FAVORITE_SCENE_ASSET_CONTENT, false, () => FavoriteScenesCache.RemoveFromFavorite(scene));
                menu.AddSeparator("Favorite/");
                menu.AddItem(MOVE_UP_FAVORITE_SCENE_ASSET_CONTENT, false, () => FavoriteScenesCache.MoveUp(scene));
                menu.AddItem(MOVE_DOWN_FAVORITE_SCENE_ASSET_CONTENT, false, () => FavoriteScenesCache.MoveDown(scene));
                menu.AddSeparator("Favorite/");
                menu.AddItem(SET_FIRST_IN_FAVORITE_SCENE_ASSET_CONTENT, false, () => FavoriteScenesCache.SetFirst(scene));
                menu.AddItem(SET_LAST_IN_FAVORITE_SCENE_ASSET_CONTENT, false, () => FavoriteScenesCache.SetLast(scene));
            }
            else
            {
                menu.AddItem(ADD_TO_FAVORITE_SCENE_ASSET_CONTENT, false, () => FavoriteScenesCache.AddToFavorite(scene));
            }
        }
    }
}
