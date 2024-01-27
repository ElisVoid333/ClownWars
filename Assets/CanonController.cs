using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
               
    }

    /* CANON BUTTON CONTROLS */
    //Rotate Canon Upwards
    public void rotateUp()
    {
        Debug.Log("Rotate Upwards");
        transform.Rotate(0f, 0.0f, 10.0f, Space.World);
    }
    //Rotate Canon Downwards
    public void rotateDown()
    {
        Debug.Log("Rotate Downwards");
        transform.Rotate(0f, 0.0f, -10.0f, Space.World);
    }

}
