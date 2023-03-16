using System;
using UnityEngine;

namespace HappyTroll
{
    public static class EventManager
    {
        public static event Action OnGameStartEvent;

        public static void OnGameStart()
        {
            OnGameStartEvent?.Invoke();
        }

        public static event Action OnGameOverEvent;

        public static void OnGameOver()
        {
            OnGameOverEvent?.Invoke();
        }

        public static event Action<Scenes> OnSceneLoadedEvent;

        public static void OnSceneLoaded(Scenes scene)
        {
            OnSceneLoadedEvent?.Invoke(scene);
        }

        public static event Action OnSceneTransitionCompletedEvent;

        public static void OnSceneTransitionCompleted()
        {
            OnSceneTransitionCompletedEvent?.Invoke();
        }
    }
}