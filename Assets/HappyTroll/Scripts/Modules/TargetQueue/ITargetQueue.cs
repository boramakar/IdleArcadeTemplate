using System.Collections.Generic;
using UnityEngine;

namespace HappyTroll
{
    public interface ITargetQueue
    {
        public void AddTarget(ITargetable target);
        public void RemoveTarget(ITargetable target);
        public ITargetable GetCurrentTarget();
        public List<ITargetable> GetNextTarget(int count = 1);
    }
}