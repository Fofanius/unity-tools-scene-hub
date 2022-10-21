using UnityEditor;

namespace SceneHub.Editor.UserSettings
{
    public abstract class UserSettingsAsset<T> : ScriptableSingleton<T> where T : ScriptableSingleton<T>
    {
        internal void SaveChanges()
        {
            Save(true);
        }
    }
}
