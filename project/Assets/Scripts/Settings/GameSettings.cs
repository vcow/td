﻿#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif
using UnityEngine;

namespace Settings
{
    public class GameSettings : ScriptableObject
    {
        // TODO: Add game presets here
        
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Game Settings", false, 10000)]
        private static void GetAndSelectSettingsInstance()
        {
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = GetSettingsInstance();
        }

        public static GameSettings GetSettingsInstance()
        {
            GameSettings instance = null;
            if (!AssetDatabase.FindAssets("t:GameSettings").Any(guid =>
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                instance = AssetDatabase.LoadAssetAtPath<GameSettings>(path);
                return true;
            }))
            {
                instance = CreateInstance<GameSettings>();
                // TODO: Initialize here

                AssetDatabase.CreateAsset(instance, "Assets/GameSettings.asset");
                AssetDatabase.SaveAssets();
            }

            return instance;
        }
#endif
    }
}