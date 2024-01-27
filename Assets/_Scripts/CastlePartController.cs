using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CastlePartController : MonoBehaviour
{
    private Rigidbody body;
    public bool isHit = false;
    public bool couldExplode = false;
    public float killTime = 3.0f;

    private float groundTimer = 0.0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (other.tag == "Explosiomn")
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
