using UnityEngine;
#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif

namespace Settings
{
    public class GameSettings : ScriptableObject
    {
        // TODO: Add game presets here

        [Header("Money")]
        public decimal StartMoney = 1000;

        [Header("Towers")]
        public GameObject TinnyTowerPrefab;
        public GameObject SmallTowerPrefab;
        public GameObject MediumTowerPrefab;
        public GameObject LargeTowerPrefab;
        public GameObject HugeTowerPrefab;
        
        [Header("Landscape")]
        public GameObject RockPrefab;
        
        [Header("Emitter")]
        public GameObject EmitterPrefab;
        
        [Header("Target")]
        public GameObject TargetPrefab;
        public int TargetHealth = 3;
        
        [Header("Enemy Wave")]
        public float EmissionSpeed = 5f;
        public int SmallEnemiesInWave = 20;
        public int MediumEnemiesInWave = 10;
        public int LargeEnemiesInWave = 5;
        public int NumberOfWaves = 3;
        public float NextWaveDelayTime = 3;

        [Header("Levels")]
        public string[] LevelAssetNames;

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