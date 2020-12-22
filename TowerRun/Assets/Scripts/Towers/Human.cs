using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Transform fixationPoint;

    public Transform FixationPoint => fixationPoint;
}
