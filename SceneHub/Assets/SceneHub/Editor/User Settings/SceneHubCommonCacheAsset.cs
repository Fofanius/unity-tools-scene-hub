using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor.UserSettings
{
    [FilePath("UserSettings/SceneHub/Common.asset", FilePathAttribute.Location.ProjectFolder)]
    public class SceneHubCommonCacheAsset : ScriptableSingleton<SceneHubCommonCacheAsset>
    {
        [SerializeField] private PopupTabs _selectedTab;
        
        public PopupTabs SelectedTab
        {
            get => _selectedTab;
            set => _selectedTab = value;
        }
    }
}
