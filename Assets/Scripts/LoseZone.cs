using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class LoseZone : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    public void Play()
    { 
        _audioSource.Play();
    }
}
