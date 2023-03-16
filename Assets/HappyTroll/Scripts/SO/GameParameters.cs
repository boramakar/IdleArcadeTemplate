using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace HappyTroll
{
    [CreateAssetMenu(menuName = "HappyTroll/GameParameters", fileName = "GameParameters")]

    public class GameParameters : SerializedScriptableObject
    {
        public float fadeAnimationDuration = 0.5f;
    }
}
