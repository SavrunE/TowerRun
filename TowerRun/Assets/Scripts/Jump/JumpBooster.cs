using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBooster : MonoBehaviour
{
    [Range(1.5f,5f)]
    [SerializeField] private float jumpMultiplication;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Jumper jumper))
        {
            jumper.MultiplicationJump(jumpMultiplication);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Jumper jumper))
        {
            jumper.ReturnJumpForce();
        }
    }
} 
