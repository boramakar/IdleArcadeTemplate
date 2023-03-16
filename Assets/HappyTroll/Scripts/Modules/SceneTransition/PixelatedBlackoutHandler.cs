using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;
using HappyTroll;

public class PixelatedBlackoutHandler : MonoBehaviour, IBlackoutHandler
{
    [SerializeField] private Texture2D blackoutTexture;
    [SerializeField] private GameObject blackoutObject;

    private int _pixelCount;
    private Color _visibleColor;
    private Color _invisibleColor;
    private List<Tuple<int, int>> _visiblePixels;
    private List<Tuple<int, int>> _invisiblePixels;
    private float _cellFadeDuration;

    private void Awake()
    {
        _pixelCount = blackoutTexture.height * blackoutTexture.width;
        _visibleColor = new Color(0, 0, 0, 1);
        _invisibleColor = new Color(0, 0, 0, 0);
        _visiblePixels = new List<Tuple<int, int>>(_pixelCount);
        _invisiblePixels = new List<Tuple<int, int>>(_pixelCount);
        _cellFadeDuration = GameManager.Instance.parameters.fadeAnimationDuration;

        for (int i = 0; i < blackoutTexture.width; i++)
        {
            for (int j = 0; j < blackoutTexture.height; j++)
            {
                _visiblePixels.Add(new Tuple<int, int>(i, j));
                blackoutTexture.SetPixel(i, j, _visibleColor);
            }
        }

        blackoutTexture.Apply();
    }

    private void Start()
    {
        DOVirtual.DelayedCall(1, () => FadeOut(null));
    }

    [Button]
    public void FadeOut(Action callback)
    {
        callback += () => blackoutObject.SetActive(false);
        StartCoroutine(_Fade(_visiblePixels, _invisiblePixels, _visibleColor, _invisibleColor, callback));
    }

    [Button]
    public void FadeIn(Action callback)
    {
        blackoutObject.SetActive(true);
        StartCoroutine(_Fade(_invisiblePixels, _visiblePixels, _invisibleColor, _visibleColor, callback));
    }

    private IEnumerator _Fade(List<Tuple<int, int>> fullList, List<Tuple<int, int>> emptyList, Color initialColor,
        Color targetColor, Action callback)
    {
        while (fullList.Count > 0)
        {
            var index = Random.Range(0, fullList.Count);
            var cell = fullList[index];
            DOVirtual.Color(initialColor, targetColor, _cellFadeDuration, (color) =>
            {
                blackoutTexture.SetPixel(cell.Item1, cell.Item2, color);
                blackoutTexture.Apply();
            });
            fullList.Remove(cell);
            emptyList.Add(cell);
            yield return null;
        }
        
        DOVirtual.DelayedCall(_cellFadeDuration, () => callback?.Invoke());
    }
}
