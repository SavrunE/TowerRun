using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float jumpForce;

    private bool isGround;
    private Rigidbody body;

    private void Start()
    {
        isGround = true;
        body = GetComponent<Rigidbody>();   
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isGround)
        {
            isGround = false;
            body.AddForce(Vector3.up * jumpForce);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Road road))
        {
            isGround = true;
        }   
    }
}
