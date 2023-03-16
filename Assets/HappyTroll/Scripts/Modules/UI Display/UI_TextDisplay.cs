using System;
using TMPro;
using UnityEngine;

namespace HappyTroll
{
    public class UI_TextDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private string id;
        [SerializeField] private String prefix;
        [SerializeField] private String suffix;
        [SerializeField] private bool autoUpdate;

        private AutoUpdateManager _autoUpdateManager;

        private void Awake()
        {
            _autoUpdateManager = AutoUpdateManager.Instance;
        }

        private void OnEnable()
        {
            _autoUpdateManager.AddDisplay(id, UpdateText);
        }

        private void OnDisable()
        {
            _autoUpdateManager.RemoveDisplay(id);
        }

        private void UpdateText(string value)
        {
            if (!autoUpdate) return;

            textMesh.text = $"{prefix}{value}{suffix}";
        }
    }
}