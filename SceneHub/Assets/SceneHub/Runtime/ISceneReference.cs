namespace SceneHub
{
    public interface ISceneReference
    {
        bool IsValid { get; }
        string ScenePath { get; }
    }
}
