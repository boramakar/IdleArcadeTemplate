using System;
using System.Collections;
using System.Collections.Generic;
using HappyTroll;
using UnityEngine;

namespace HappyTroll
{
    public class GameManager : Singleton<GameManager>
    {
        public GameParameters parameters;

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
