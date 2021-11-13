using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _startZPosition;
    [SerializeField] private PointsText _pointsText;
    [SerializeField] private Stickman _stickman;
    [SerializeField] private CanvasGroup _loseTextCanvasGroup;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Water water))
        {
            if (transform.position.z > _startZPosition)
            {
                water.CreateParticle(transform);
            }
        }
        
        if (collider.TryGetComponent(out Platform platform))
        {
            _pointsText.Create(platform.PointsValue);
        }
        
        if (collider.TryGetComponent(out LoseZone loseZone))
        {
            loseZone.Play();
            Lose();
        }
    }

    private void Lose()
    {
        _stickman.Rotate();
        _loseTextCanvasGroup.alpha = 1;
    }
}
