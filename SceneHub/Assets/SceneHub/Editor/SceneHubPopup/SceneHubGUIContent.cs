using UnityEngine;

namespace SceneHub.Editor
{
    internal static class SceneHubGUIContent
    {
        internal static class SceneOptions
        {
            internal static readonly GUIContent SwitchSceneAndPlay = new GUIContent("▶", "Switch scene and start PlayMode.");
            internal static readonly GUIContent ContextMenu = new GUIContent("☰", "Additional options for current scene.");
        }

        internal static class FavoriteMenu
        {
            internal const string FAVORITE_MENU_CATEGORY = "Favorite";
            internal const string FAVORITE_MENU_CATEGORY_WITH_MENU_SEPARATOR = "Favorite/";

            internal static readonly GUIContent AddToFavorites = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Add", "Add to favorites.");
            internal static readonly GUIContent MoveUpInFavoriteList = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Move Up", "Move up in favorites.");
            internal static readonly GUIContent MoveDownInFavoriteList = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Move Down", "Move down in favorites.");
            internal static readonly GUIContent RemoveFromFavorites = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Remove", "Remove from favorites.");
            internal static readonly GUIContent SetAsFirstInFavorites = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Set as First", "Move scene to the top of favorites.");
            internal static readonly GUIContent SetAsLastInFavorites = new GUIContent($"{FAVORITE_MENU_CATEGORY}/Set as Last", "Move scene to the bottom of favorites.");
        }

        internal static class SceneLibraryOptions
        {
            internal static readonly GUIContent LoadAllScenes = new GUIContent("↡", "Load all scenes.");
        }

        internal static class SceneMenu
        {
            internal static readonly GUIContent PingSceneAsset = new GUIContent("Ping", "Ping scene in Project Tab.");
        }

        internal static class BuildListMenu
        {
            internal const string BUILD_LIST_MENU_CATEGORY = "Build list";

            internal static readonly GUIContent AddToBuildList = new GUIContent($"{BUILD_LIST_MENU_CATEGORY}/Add", "Add to build scene list.");
            internal static readonly GUIContent RemoveFromBuildList = new GUIContent($"{BUILD_LIST_MENU_CATEGORY}/Remove", "Remove from build scene list.");
            internal static readonly GUIContent EnableInBuildList = new GUIContent($"{BUILD_LIST_MENU_CATEGORY}/Enable", "Enable scene in build scene list.");
            internal static readonly GUIContent DisableInBuildList = new GUIContent($"{BUILD_LIST_MENU_CATEGORY}/Disable", "Disable scene in build scene list.");
        }
    }
}
