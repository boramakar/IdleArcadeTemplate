using UnityEngine;

namespace HappyTroll
{
    public interface ITargetable
    {
        public GameObject GetGameObject();
        public TargetableType GetTargetType();
    }
}