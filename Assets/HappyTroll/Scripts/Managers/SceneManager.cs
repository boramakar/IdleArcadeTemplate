using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HappyTroll
{
    public class SceneManager : Singleton<SceneManager>
    {
        private IBlackoutHandler blackoutHandler;
        private Scenes _currentScene;

        private void Awake()
        {
            blackoutHandler = GetComponentInChildren<IBlackoutHandler>();
            _currentScene = Scenes.LoginScene;
        }

        public void ChangeScene(string sceneName)
        {
            _currentScene = sceneName switch
            {
                Strings.LoginSceneName => Scenes.LoginScene,
                Strings.GameSceneName => Scenes.GameScene,
                _ => throw new ArgumentException("Scene with given name not found", nameof(sceneName))
            };
            blackoutHandler.FadeIn(() =>
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName).completed += OnSceneLoaded);
        }

        public void ChangeScene(int sceneIndex)
        {
            _currentScene = (Scenes) sceneIndex;
            blackoutHandler.FadeIn(() =>
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex).completed += OnSceneLoaded);
        }

        private void OnSceneLoaded(AsyncOperation operation)
        {
            EventManager.OnSceneLoaded(_currentScene);
            blackoutHandler.FadeOut(EventManager.OnSceneTransitionCompleted);
        }
    }

}