# Scene Hub

Source code - [github-page](https://github.com/Fofanius/SceneHub).

Allows dev to avoid using *hardcoded magic number*, *hardcoded strings*, *in-code constants* to reference/manage any scene.

## Scene Hub Popup

Is in-editor popup, that opens using `ctrl + alt + q` shortcut manage all `Scenes` / `Scene Reference`-s / `Scene Library`-es`.

Popup content and logic separated by different tabs.

### Scenes Tab

Displays list of all project scenes.

### Favorites

Displays list of all scenes marked as *favorite*.

### References

Displays list of all project `Scene Reference`-s.

### Libraries

Displays list of all project `Scene Library`-s.

## Scene Reference

Scriptable reference to `Scene` in project. 

### Project usage

Use `Create` menu `Scene Hub/Scene Reference` to create reference asset and assign desired `Scene`.

You also can *double-click* it to load scene in editor.

### Code usage

**Sync load:**

``` c#
[SerializeField] private SceneReferenceAsset _nextLevel;

// ..

private void OnLevelComplete()
{
     // ..
     // logic here
     // ..

     _nextLevel.Load();
}
```

**Async load:**

``` c#
[SerializeField] private SceneReferenceAsset _nextLevel;

// ..

private void OnLevelComplete()
{
     // ..
     // logic here
     // ..

     StartCoroutine(LoadNextLevelAsyncRoutine());
}

private IEnumerator LoadNextLevelAsyncRoutine()
{
    yield return _nextLevel.LoadAsync();
}

```

## Scene Library

`Scene Library` is scriptable collection of `Scene Reference`-s. Allow dev to load all defined `Scene`-s at once.

### Project Usage

Use `Create` menu `Scene Hub/Scene Library` to create library asset and assign desired `Scene Reference`-s.

You also can *double-click* it to load all scenes in editor.

### Code usage

**Sync load:**

``` c#
[SerializeField] private SceneLibraryAsset _gameLevels;

// ..

private void StartGame()
{
     // ..
     // logic here
     // ..

     _gameLevels.Load();
}
```

**Async load:**

``` c#
[SerializeField] private SceneLibraryAsset _gameLevels;

// ..

private void StartGame()
{
     // ..
     // logic here
     // ..

     StartCoroutine(LoadGameLevelsAsyncRoutine());
}

private IEnumerator LoadGameLevelsAsyncRoutine()
{
     yield return _gameLevels.LoadAsync();
}
```
