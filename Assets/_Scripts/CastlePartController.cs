/////////////////////////////////////////////////////////////////////////////////////////////////////
//
//   Project - CLOWN WARS
//   Filename - CastlePartControler.cs
//   Author - Eric DeMarbre
//   Date - January 27 2024
//
//   Description - Controls the behaviour of the castle parts for Clown Wars, particularly physics  
//   and collision behaviour. 
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CastlePartController : MonoBehaviour
{
    public float killTime = 3.0f;
    public bool isHit = false;
    public bool couldExplode = false;

    private Rigidbody body;
    public float groundTimer = 0.0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (GetComponent<Rigidbody>().IsSleeping())
        {
            GetComponent<Rigidbody>().WakeUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Clown")
        {
            body.constraints = RigidbodyConstraints.None;
            isHit = true;
        }

        if (other.tag == "PlayArea")
        {
            DestroyPiece();
        }

        if (other.tag == "Explosion")
        {
            couldExplode = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Explosion")
        {
            couldExplode = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;

        if (hit.tag == "Castle")
        {
            if (hit.GetComponent<CastlePartController>().isHit)
            {
                body.constraints = RigidbodyConstraints.None;
                isHit = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject hit = collision.gameObject;

        if (hit.tag == "Ground" && isHit)
        {
            groundTimer += Time.deltaTime;

            if(groundTimer >= killTime)
            {
                DestroyPiece();
            }
        }
    }

    public void Reset()
    {
        couldExplode = false;
    }

    private void DestroyPiece()
    {
        Destroy(this.gameObject);
    }
}
