using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableBottle : MonoBehaviour
{
    public Vector3 startingPosition;   // 시작 위치
    public Quaternion startingRotation;    // 시작 방향

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            startingPosition = transform.position;
            startingRotation = transform.rotation;
        }
        else
        {
            startingPosition = rb.position;
            startingRotation = rb.rotation;
        }
    }

    public void Respawn()
    {
        if (rb ==  null)
        {
            transform.position = startingPosition;
            transform.rotation = startingRotation;
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.position = startingPosition;
            rb.rotation = startingRotation;
        }
    }
}
