using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GoToApps.ObjectsPool.Editor
{
    [CustomEditor(typeof(PoolManager))]
    [CanEditMultipleObjects]
    public class PoolManagerEditor : UnityEditor.Editor
    {
        private PoolManager _model;

        private SerializedProperty _poolParentTransform;
        private SerializedProperty _poolPrefab;
        private SerializedProperty _selfInitialize;
        private SerializedProperty _initializePoolSize;
        private SerializedProperty _initializeIn;
        private SerializedProperty _showDebugLogs;
        
        private void OnEnable()
        {
            _model = (PoolManager) target;

            _poolParentTransform = serializedObject.FindProperty("_poolParentTransform");
            _poolPrefab = serializedObject.FindProperty("_poolPrefab");
            _selfInitialize = serializedObject.FindProperty("_selfInitialize");
            _initializePoolSize = serializedObject.FindProperty("_initializePoolSize");
            _initializeIn = serializedObject.FindProperty("_initializeIn");
            _showDebugLogs = serializedObject.FindProperty("_showDebugLogs");
        }

        private void DrawFields()
        {
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            
            EditorGUILayout.PropertyField(_selfInitialize);
            bool selfInitialize = _selfInitialize.boolValue;
            if (selfInitialize)
            {
                EditorGUILayout.PropertyField(_poolParentTransform);
                EditorGUILayout.PropertyField(_poolPrefab);
                EditorGUILayout.PropertyField(_initializePoolSize);
                EditorGUILayout.PropertyField(_initializeIn);
            }

            EditorGUILayout.PropertyField(_showDebugLogs);
        }

        private void OnChanged(GameObject obj)
        {
            if (Application.isPlaying == false)
            {
                EditorUtility.SetDirty(obj);
                EditorSceneManager.MarkSceneDirty(obj.scene);
            }
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawFields();
            serializedObject.ApplyModifiedProperties();
            if (GUI.changed) OnChanged(_model.gameObject);
        }
    }
}