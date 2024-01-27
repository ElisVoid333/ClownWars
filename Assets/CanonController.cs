using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    /* Rotation of Canon Variables */
    
    public float rotationSpeed;         //How fast barrel will rotate along z-axis

    public bool rotateCanonUpwards;     //Allow rotation upwards
    public bool rotateCanonDownwards;   //Allow rotation downwards

    private float maxRotation = 43.5f;    //Restrict rotation upwards     (Max Value barrel may rotate)
    private float minRotation = 300f;    //Restrict rotation downwards    (Min Value barrel may rotate)

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(transform.eulerAngles.z);

        if (rotateCanonUpwards)
        {
            transform.Rotate(0f, 0.0f, rotationSpeed, Space.World);
        }
        if (rotateCanonDownwards)
        {
            transform.Rotate(0f, 0.0f, -rotationSpeed, Space.World);
        }

        if (transform.eulerAngles.z >= maxRotation && transform.eulerAngles.z < 100f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, maxRotation);
        }
        if (transform.eulerAngles.z <= minRotation && transform.eulerAngles.z > 100f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, minRotation);
        }
    }

    /* CANON BUTTON CONTROLS */
    //Allow to rotate Canon Upwards
    public void rotateUp(bool _rotateUp)
    {
        Debug.Log("Rotate Upwards");
        rotateCanonUpwards = _rotateUp;
    }
    //Allow to rotate Canon Downwards
    public void rotateDown(bool _rotateDown)
    {
        Debug.Log("Rotate Downwards");
        rotateCanonDownwards = _rotateDown;
    }

}
