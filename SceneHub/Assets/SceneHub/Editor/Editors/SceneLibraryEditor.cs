using UnityEditor;
using UnityEngine;

namespace SceneHub
{
    [CustomEditor(typeof(SceneLibrary))]
    public class SceneLibraryEditor : Editor
    {
        private SerializedProperty _title;
        private SerializedProperty _list;
        private SerializedProperty _order;

        private Vector2 _scroll;

        private void OnEnable()
        {
            _title = serializedObject.FindProperty("_title");
            _list = serializedObject.FindProperty("_scenes");
            _order = serializedObject.FindProperty("_sortingOrder");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_title);
            EditorGUILayout.PropertyField(_order);
            EditorGUILayout.Space();

            DrawList();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawList()
        {
            if (_list == default || !_list.isArray) return;

            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            {
                for (int i = 0; i < _list.arraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                    {
                        // информация о сцене
                        EditorGUILayout.BeginVertical();
                        {
                            var element = _list.GetArrayElementAtIndex(i);

                            EditorGUILayout.PropertyField(element.FindPropertyRelative("_title"));
                            EditorGUILayout.PropertyField(element.FindPropertyRelative("_sceneAsset"));
                        }
                        EditorGUILayout.EndVertical();

                        // управление
                        EditorGUILayout.BeginVertical(GUILayout.Width(70));
                        {
                            GUI.enabled = i > 0;
                            if (GUILayout.Button("Move up")) _list.MoveArrayElement(i, i - 1);
                            GUI.enabled = i < _list.arraySize - 1;
                            if (GUILayout.Button("Mode down")) _list.MoveArrayElement(i, i + 1);
                            GUI.enabled = true;

                            GUI.color = Color.red;
                            {
                                if (GUILayout.Button("Delete"))
                                {
                                    _list.DeleteArrayElementAtIndex(i);
                                }
                            }
                            GUI.color = Color.white;
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                }

                if (GUILayout.Button("Add"))
                {
                    _list.arraySize++;
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}
