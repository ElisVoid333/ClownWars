/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// ****************Template**********************
//
//   Project - CLOWN WARS
//   Filename - CameraControler.cs
//   Author - Alex Nita
//   Date - January 28 2024
//
//   Description - Controls the behaviour of camera 
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Scrollbar scrolleebar;       //ScrollBar
    public bool trackingClown;          //bool to change camera control from tracking clown or default

    float minCamPos;                    //Position of camera when beginning
    float maxCamPos;                    //Position of camera of furthest point

    // Start is called before the first frame update
    void Start()
    {
        minCamPos = 3.962f;             //Default Spot
        maxCamPos = 50.0f - minCamPos;  // this will get difference between the two, necessary for having a minCamPos
        trackingClown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (trackingClown)
        {
            Debug.Log("Tracking The CLoon");
            trackClown();
        }
        else
        {
            float newCamPos = (maxCamPos * scrolleebar.value) + minCamPos;

            transform.position = new Vector3(newCamPos, transform.position.y, transform.position.z);
        }
    }

    //Track Object when Launched
    public void trackClown()
    {
        GameObject target = TestClownController.instance.CurrentClown;

        target = findChild(target.transform);

        Debug.Log(target);
        transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
    }
    
    private GameObject findChild(Transform parent)
    {
        GameObject child = null;

        foreach(Transform transform in parent.transform) {
            if(transform.CompareTag(tag)) {
                child = transform.gameObject;
                break;
            }
        }
        return child;
    }

    public void setTrackingClown(bool yayOrNay)
    {
        trackingClown = yayOrNay;
    }
}
