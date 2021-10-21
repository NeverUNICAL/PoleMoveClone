using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(TextMeshProUGUI))]

public class PointsText : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _startYPosition;
    [SerializeField] private float _targetYPosition;

    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Create(float pointsAmount)
    {
        _text.text ="+20";
        _canvasGroup.alpha = 1;
        _canvasGroup.DOFade(0, _lifeTime);
        transform.DOMoveY(transform.position.y + _targetYPosition, _lifeTime);
        transform.DOMoveY(_startYPosition, 0);
    }
}
