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
        private SerializedProperty _dontDestroyOnLoad;
        private SerializedProperty _poolManagerDataPlayerPrefsKey;
        private SerializedProperty _selfInitialize;
        private SerializedProperty _initializeIn;
        private SerializedProperty _initializeMode;
        private SerializedProperty _poolPrefab;
        private SerializedProperty _prefabs;
        private SerializedProperty _initializePoolSize;
        private SerializedProperty _iterationsCount;
        private SerializedProperty _createAllObjects;
        private SerializedProperty _showDebugLogs;
        private SerializedProperty _showPoolInitializerLogs;
        private SerializedProperty _showPoolOperationLogs;
        private SerializedProperty _showBackgroundControlLogs;
        
        
        private void OnEnable()
        {
            _model = (PoolManager) target;

            _poolParentTransform = serializedObject.FindProperty("_poolParentTransform");
            _dontDestroyOnLoad = serializedObject.FindProperty("_dontDestroyOnLoad");
            _poolManagerDataPlayerPrefsKey = serializedObject.FindProperty("_poolManagerDataPlayerPrefsKey");
            _selfInitialize = serializedObject.FindProperty("_selfInitialize");
            _initializeIn = serializedObject.FindProperty("_initializeIn");
            _initializeMode = serializedObject.FindProperty("_initializeMode");
            _poolPrefab = serializedObject.FindProperty("_poolPrefab");
            _prefabs = serializedObject.FindProperty("_prefabs");
            _initializePoolSize = serializedObject.FindProperty("_initializePoolSize");
            _iterationsCount = serializedObject.FindProperty("_iterationsCount");
            _createAllObjects = serializedObject.FindProperty("_createAllObjects");
            _showDebugLogs = serializedObject.FindProperty("_showDebugLogs");
            _showPoolOperationLogs = serializedObject.FindProperty("_showPoolOperationLogs");
            _showPoolInitializerLogs = serializedObject.FindProperty("_showPoolInitializerLogs");
            _showBackgroundControlLogs = serializedObject.FindProperty("_showBackgroundControlLogs");
        }

        private void DrawFields()
        {
            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_poolParentTransform);

            EditorGUILayout.PropertyField(_dontDestroyOnLoad);
            bool dontDestroyOnLoad = _dontDestroyOnLoad.boolValue;
            if (dontDestroyOnLoad) EditorGUILayout.PropertyField(_poolManagerDataPlayerPrefsKey);
            
            
            EditorGUILayout.PropertyField(_selfInitialize);
            bool selfInitialize = _selfInitialize.boolValue;
            if (selfInitialize)
            {
                EditorGUILayout.PropertyField(_initializeMode);
                SelfInitializeMode initializeMode = (SelfInitializeMode) _initializeMode.enumValueIndex;
                switch (initializeMode)
                {
                    case SelfInitializeMode.SinglePrefab:
                        EditorGUILayout.PropertyField(_poolPrefab);
                        EditorGUILayout.PropertyField(_initializePoolSize);
                        break;
                    
                    case SelfInitializeMode.MultiplePrefabs:
                        EditorGUILayout.PropertyField(_prefabs);
                        EditorGUILayout.PropertyField(_createAllObjects);
                        bool createAllObjects = _createAllObjects.boolValue;
                        EditorGUILayout.PropertyField(createAllObjects ? _iterationsCount : _initializePoolSize);
                        break;
                    
                    case SelfInitializeMode.InitializeTemplate:
                        EditorGUILayout.PropertyField(_prefabs);
                        break;
                }
                EditorGUILayout.PropertyField(_initializeIn);
            }

            EditorGUILayout.PropertyField(_showDebugLogs);
            bool showDebugLogs = _showDebugLogs.boolValue;
            if (showDebugLogs)
            {
                EditorGUILayout.PropertyField(_showPoolInitializerLogs);
                EditorGUILayout.PropertyField(_showPoolOperationLogs);
                EditorGUILayout.PropertyField(_showBackgroundControlLogs);
            }
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