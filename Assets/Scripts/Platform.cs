using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
   [SerializeField] private float _pointsValue;

   public float PointsValue => _pointsValue;
}
