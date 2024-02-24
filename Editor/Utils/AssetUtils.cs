using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EcoMine.Editor.Utils
{
    public class AssetUtils
    {
        public static List<T> LoadAllAssetAtPath<T>(string path, string searchPattern = "") where T : Object
        {
            string[] files;
            if(!string.IsNullOrEmpty(searchPattern))
                files = Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
            else
                files = Directory.GetFiles(path);
            
            List<T> objects = new List<T>();
            foreach (var file in files)
                if(!file.EndsWith(".meta"))
                    objects.Add(AssetDatabase.LoadAssetAtPath<T>(file));
            
            return objects;
        }
        
        public static T CreateScriptTableObject<T>(string name, string path, bool saveAndRefresh = true) where T : ScriptableObject
        {
            T stageData = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(stageData, Path.Combine(path, $"{name}.asset"));
            if(saveAndRefresh) SaveAssetsAndRefresh(stageData);
            return stageData;
        }
        
        public static ScriptableObject CreateScriptTableObject(Type type,string name, string path, bool saveAndRefresh = true)
        {
            ScriptableObject stageData = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(stageData, Path.Combine(path, $"{name}.asset"));
            if(saveAndRefresh) SaveAssetsAndRefresh(stageData);
            return stageData;
        }
        
        public static GameObject CreatePrefab(string pathSave, GameObject objBase)
        {
            GameObject instanceRoot = (GameObject)PrefabUtility.InstantiatePrefab(objBase);
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(instanceRoot, pathSave);
            Object.DestroyImmediate(instanceRoot);
            return prefab;
        }

        public static void SaveAssetsAndRefresh(Object saveObject)
        {
            EditorUtility.SetDirty(saveObject);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}