using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventSystem))]
public class DuplicateEventSystemDisabler : MonoBehaviour
{
    private void OnEnable()
    {
        var target = GetComponent<EventSystem>();
        var current = EventSystem.current;

        if (!current || !target || current == target) return;

        target.enabled = false;
        
        // DestroyImmediate(GetComponent<BaseInputModule>());
        // DestroyImmediate(target);
    }
}
