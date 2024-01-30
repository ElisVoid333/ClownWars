/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// ****************Template**********************
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
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanonController : MonoBehaviour
{
    public Scrollbar scrollbar;

    /*-- Instances --*/
    public GameObject clown;
    public GameObject barrel;

    /*-- Rotation of Canon Variables --*/
    public float rotationSpeed;                                         //How fast barrel will rotate along z-axis

    private bool rotateCanonUpwards;                                    //Allow rotation upwards
    private bool rotateCanonDownwards;                                  //Allow rotation downwards

    private float maxRotation = -10.0f;                                  //Restrict rotation upwards     (Max Value barrel may rotate)
    private float minRotation = -85.0f;                                   //Restrict rotation downwards    (Min Value barrel may rotate)


    /*-- Firing Canon Variables --*/
    public GameObject frontOfBarrel;                                    //Front Of Barrel Object

    private List<GameObject> ammo = new List<GameObject>();            //List of all ammo to shoot out of cannon out of GameObject (Takes GameObject)
    //private int[] ammo;                                                 //List of all ammo to shoot out of cannon out of int (counter)

    //private bool addingThrustingPower;                                  //Adds thrust force as player holds down button
    //public float thrustForce;                                           //Force of objects flung out of cannon
    //public float maxThrustForce;                                        //Maximum thrust to flung objects out of cannon
    //public float minThrustForce;                                        //Minimum thrust to flung objects out of cannon


    // Start is called before the first frame update
    void Start()
    {
        //Rotation Values Initialized
        rotationSpeed = 0.05f;

        //Thrust Values Initialized
        //thrustForce = 0f;
        //maxThrustForce = 15f;
        //minThrustForce = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        float newAngle = minRotation + ((maxRotation - minRotation) * scrollbar.value);

        Quaternion toAngle = Quaternion.Euler(0.0f, 0.0f, newAngle);

        barrel.transform.rotation = Quaternion.Lerp(barrel.transform.rotation, toAngle, Time.deltaTime / rotationSpeed);

        //Vector3 toAngle = new Vector3(0.0f, 0.0f, newAngle);

        //transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, toAngle, Time.deltaTime);

        //transform.eulerAngles = toAngle;


        /*-- Rotation of Canon
        //Debug.Log(transform.eulerAngles.z);

        // Rotates Canon Upwards if button held down
        if (rotateCanonUpwards || Input.GetKey(KeyCode.W))
        {
            transform.Rotate(0f, 0.0f, rotationSpeed, Space.World);
        }
        // Rotates Canon Downwards if button held down
        if (rotateCanonDownwards || Input.GetKey(KeyCode.S))
        {
            transform.Rotate(0f, 0.0f, -rotationSpeed, Space.World);
        }

        //Restricts Rotation Upward
        if (transform.eulerAngles.z >= maxRotation && transform.eulerAngles.z < 100f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, maxRotation);
        }
        //Restricts Rotation Downward
        if (transform.eulerAngles.z <= minRotation && transform.eulerAngles.z > 100f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, minRotation);
        }
 
        /*-- Firing Canon --*/
        //if (addingThrustingPower)
        //{
        //    thrustForce += 0.01f;

        //    if (thrustForce >= maxThrustForce)
        //    {
        //        thrustForce = maxThrustForce;
        //    }
            //Debug.Log(thrustForce);
        //}

        /*-- For Testing --*/
        //Spawns Ball on top of collider
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    Instantiate(clown, new Vector3(-6.55f, 5.32f, -1.5f), Quaternion.identity);
        //}
    }


    /*-- CANON BUTTON CONTROLS --*/
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
    //Allow to add power to cannon shot
    //public void addThrust(bool _addThrust)
    //{
    //    Debug.Log("Thrusting");
    //    addingThrustingPower = _addThrust;
    //}
    //Fires Cannon
    //public void fire()
    //{
    //    Debug.Log("FIIIIIRRREEE!");
    //    //Debug.Log(thrustForce);

    //    grabAmmo();
    //    shoot();

    //    ammo = new List<GameObject>();  //Resets List
    //    //thrustForce = minThrustForce;   //Resets force applied to objects when shot
    //}


    /*-- Canon Functions --*/
    //Puts all ammo in front of barrel
    private void grabAmmo()
    {
        //ammo = GameObject.FindGameObjectsWithTag("Clown");

        for (int i = 0; i < ammo.Count; i++)
        {
            ammo[i].SetActive(true);
            //Instantiate(clown, frontOfBarrel.transform.position, Quaternion.identity);

            ammo[i].gameObject.transform.position = frontOfBarrel.transform.position;
        }
    }
    //Shoots all objects in GameObject[] ammo
    //private void shoot()
    //{
        // ammo = GameObject.FindGameObjectsWithTag("Clown");

    //    for (int i = 0; i < ammo.Count; i++)
    //    {
    //        Rigidbody bullet = ammo[i].GetComponent<Rigidbody>();
    //        bullet.velocity = Vector3.zero; //stops any existing physics on bullet

    //        Vector3 angledShot = shootingAngle();
    //        bullet.AddForce(frontOfBarrel.transform.position * thrustForce, ForceMode.Impulse);
    //    }
    //}
    //Calculate vector to shoot objects from cannon
    //public Vector3 shootingAngle()
    //{
    //    Vector3 shootingAngle = Vector3.zero;

    //    shootingAngle = -frontOfBarrel.transform.right;
    //    shootingAngle.y = Mathf.Abs(transform.rotation.y);

        //Debug.Log(shootingAngle);
    //    return shootingAngle;
    //}


    /*-- Trigger Events --*/
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Clown")
    //    {
    //        Debug.Log("Clown has entered the cannon");
    //        ammo.Add(other.gameObject);
    //        other.gameObject.SetActive(false);
    //        Destroy(other.gameObject.transform.root);
    //    }
    //}
}
