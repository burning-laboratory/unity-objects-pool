using GoToApps.ObjectsPool.Types;
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
        private SerializedProperty _initializeType;
        private SerializedProperty _iterationsCount;
        private SerializedProperty _createAllObjects;
        private SerializedProperty _prefabs;
        private void OnEnable()
        {
            _model = (PoolManager) target;

            _poolParentTransform = serializedObject.FindProperty("_poolParentTransform");
            _poolPrefab = serializedObject.FindProperty("_poolPrefab");
            _selfInitialize = serializedObject.FindProperty("_selfInitialize");
            _initializePoolSize = serializedObject.FindProperty("_initializePoolSize");
            _initializeIn = serializedObject.FindProperty("_initializeIn");
            _showDebugLogs = serializedObject.FindProperty("_showDebugLogs");
            _initializeType = serializedObject.FindProperty("_initializeType");
            _iterationsCount = serializedObject.FindProperty("_iterationsCount");
            _createAllObjects = serializedObject.FindProperty("_createAllObjects");
            _prefabs = serializedObject.FindProperty("_prefabs");
        }

        private void DrawFields()
        {
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_poolParentTransform);
            
            EditorGUILayout.PropertyField(_selfInitialize);
            bool selfInitialize = _selfInitialize.boolValue;
            if (selfInitialize)
            {
                EditorGUILayout.PropertyField(_initializeType);
                SelfInitializeType initializeType = (SelfInitializeType) _initializeType.enumValueIndex;
                switch (initializeType)
                {
                    case SelfInitializeType.SinglePrefab:
                        EditorGUILayout.PropertyField(_poolPrefab);
                        EditorGUILayout.PropertyField(_initializePoolSize);
                        break;
                    
                    case SelfInitializeType.MultiplePrefabs:
                        EditorGUILayout.PropertyField(_prefabs);
                        EditorGUILayout.PropertyField(_createAllObjects);
                        bool createAllObjects = _createAllObjects.boolValue;
                        EditorGUILayout.PropertyField(createAllObjects ? _iterationsCount : _initializePoolSize);
                        break;
                    
                    case SelfInitializeType.InitializeTemplate:
                        EditorGUILayout.PropertyField(_prefabs);
                        break;
                }
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