using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownController : MonoBehaviour
{
    private SphereCollider selfCollider;

    // Start is called before the first frame update
    void Start()
    {
        selfCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Ignores collisions with other clowns
        if (collision.gameObject.tag == "Clown")
        {
            Physics.IgnoreCollision(collision.collider, selfCollider);
        }
    }

    private void getHighestParented()
    {

    }

    private void bodyZeroVelocity()
    {

    }
}