using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCapsuleController : MonoBehaviour
{
    public float forceSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(forceSpeed, 0.0f, 0.0f), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }
}
