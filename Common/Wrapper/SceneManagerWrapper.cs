using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace HITStudio.SceneManager
{
    public class SceneManagerWrapper
    {
        private static readonly Dictionary<string, object> _scenedata = new();
        
        public static UniTask Load(string sceneName, object data = null)
        {
            return LoadScene(sceneName, LoadSceneMode.Single, data);
        }

        public static UniTask Add(string sceneName, object data = null)
        {
            return LoadScene(sceneName, LoadSceneMode.Additive, data);
        }

        private static async UniTask LoadScene(string sceneName, LoadSceneMode loadSceneMode, object data = null)
        {
            // Check scene is loaded
            if (IsSceneLoaded(sceneName))
                throw new Exception($"Scene {sceneName} is loaded.");
            
            _scenedata.Add(sceneName, data); // Save Data
            
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            await UniTask.WaitUntil(() => asyncOperation.isDone);
            
        }

        public static async UniTask Close(string sceneName)
        {
            // Check scene is loaded
            if (!IsSceneLoaded(sceneName))
                throw new NullReferenceException($"Scene {sceneName} has not been loaded.");
            
            AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            await UniTask.WaitUntil(() => asyncOperation.isDone);
            
            _scenedata.Remove(sceneName); // Remove Data
        }

        public static T GetData<T>(string sceneName)
        {
            if (!HasData(sceneName))
                throw new NullReferenceException($"Can't get scene {sceneName} data.");
            
            return (T)_scenedata[sceneName];
        }

        public static bool HasData(string sceneName)
        {
            if (!_scenedata.ContainsKey(sceneName))
                return false;
            if (_scenedata[sceneName] == null)
                return false;
            return true;
        }

        private static bool IsSceneLoaded(string sceneName)
        {
            Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
            return scene.isLoaded || scene.IsValid();
        }
    }
}