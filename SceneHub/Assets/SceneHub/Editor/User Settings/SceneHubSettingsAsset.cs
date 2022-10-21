using UnityEditor;
using UnityEngine;

namespace SceneHub.Editor.UserSettings
{
    [FilePath("UserSettings/SceneHub/SceneHubSettings.asset", FilePathAttribute.Location.ProjectFolder)]
    public class SceneHubSettingsAsset : UserSettingsAsset<SceneHubSettingsAsset>
    {
        [SerializeField] private PopupTabs _selectedTab;
        
        public PopupTabs SelectedTab
        {
            get => _selectedTab;
            set => _selectedTab = value;
        }
    }
}
