using UnityEditor;
using UnityEngine;

namespace SceneHub
{
    [CustomEditor(typeof(SceneHubAsset))]
    public class SceneHubAssetEditor : Editor
    {
        private SerializedProperty _title;
        private SerializedProperty _list;
        private SerializedProperty _order;

        private Vector2 _scroll;

        private void OnEnable()
        {
            _title = serializedObject.FindProperty(nameof(SceneHubAsset.Title));
            _list = serializedObject.FindProperty(nameof(SceneHubAsset.Scenes));
            _order = serializedObject.FindProperty(nameof(SceneHubAsset.Order));
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

                            EditorGUILayout.PropertyField(element.FindPropertyRelative(nameof(SceneInfo.Title)));
                            EditorGUILayout.PropertyField(element.FindPropertyRelative(nameof(SceneInfo.Scene)));
                        }
                        EditorGUILayout.EndVertical();

                        // управление
                        EditorGUILayout.BeginVertical(GUILayout.Width(70));
                        {
                            GUI.enabled = i > 0;
                            if (GUILayout.Button("Поднять")) _list.MoveArrayElement(i, i - 1);
                            GUI.enabled = i < _list.arraySize - 1;
                            if (GUILayout.Button("Опустить")) _list.MoveArrayElement(i, i + 1);
                            GUI.enabled = true;

                            GUI.color = Color.red;
                            {
                                if (GUILayout.Button("Удалить"))
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

                if (GUILayout.Button("Добавить"))
                {
                    _list.arraySize++;
                }
            }
            EditorGUILayout.EndScrollView();
        }
    }
}