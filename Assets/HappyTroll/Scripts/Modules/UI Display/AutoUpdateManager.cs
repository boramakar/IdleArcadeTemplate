using System;
using System.Collections.Generic;

namespace HappyTroll
{
    public class AutoUpdateManager : Singleton<AutoUpdateManager>
    {
        private Dictionary<string, Action<string>> autoUpdateList;

        public void AddDisplay(string id, Action<string> callback)
        {
            if (autoUpdateList.ContainsKey(id)) return;
            
            autoUpdateList.Add(id, callback);
        }

        public void RemoveDisplay(string id)
        {
            autoUpdateList.Remove(id);
        }

        public void UpdateDisplay(string id, string value)
        {
            autoUpdateList[id]?.Invoke(value);
        }
    }
}