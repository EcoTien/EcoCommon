using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EcoMine.Editor
{
    internal static class AssetWrapper
    {
        public static T[] LoadAllAssetAtPath<T>(string path) where T : Object
        {
            string[] allAssetGUIDs = AssetDatabase.FindAssets("", new string[]{ path });
            List<T> items = new List<T>();
            foreach (string guid in allAssetGUIDs)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                Object asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
                if(asset is T t) items.Add(t);
            }
            return items.ToArray();
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