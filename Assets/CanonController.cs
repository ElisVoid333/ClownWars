using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    /*-- Rotation of Canon Variables --*/
    public float rotationSpeed;         //How fast barrel will rotate along z-axis

    public bool rotateCanonUpwards;     //Allow rotation upwards
    public bool rotateCanonDownwards;   //Allow rotation downwards

    private float maxRotation = 43.5f;  //Restrict rotation upwards     (Max Value barrel may rotate)
    private float minRotation = 300f;   //Restrict rotation downwards    (Min Value barrel may rotate)


    /*-- Firing Canon Variables --*/
    public GameObject frontOfBarrel;    //Front Of Barrel Object

    private GameObject[] ammo;          //List of all ammo to shoot out of cannon
    public float thrustForce;           //Force of objects flung out of cannon



    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 0.05f;
        thrustForce = 10f;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.eulerAngles.z);

        // Rotates Canon Upwards if button held down
        if (rotateCanonUpwards)
        {
            transform.Rotate(0f, 0.0f, rotationSpeed, Space.World);
        }
        // Rotates Canon Downwards if button held down
        if (rotateCanonDownwards)
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
    //Fires Cannon
    public void fire()
    {
        Debug.Log("FIIIIIRRREEE!");
        grabAmmo();
        shoot();
    }


    /*-- Canon Functions --*/
    //Grabs all objects in scene with specific tag
    private void grabAmmo()
    {
        ammo = GameObject.FindGameObjectsWithTag("Clown");

        for (int i = 0; i < ammo.Length; i++)
        {
            ammo[i].gameObject.transform.position = frontOfBarrel.transform.position;
        }
    }
    //Shoots all objects in GameObject[] ammo
    private void shoot()
    {
        ammo = GameObject.FindGameObjectsWithTag("Clown");

        for (int i = 0; i < ammo.Length; i++)
        {
            Rigidbody bullet = ammo[i].GetComponent<Rigidbody>();
            bullet.velocity = Vector3.zero; //stops any existing physics on bullet

            Vector3 angledShot = shootingAngle();
            
            bullet.AddRelativeForce(frontOfBarrel.transform.position * thrustForce, ForceMode.Impulse);
        }
    }
    //Calculate vector to shoot objects from cannon
    public Vector3 shootingAngle()
    {
        Vector3 shootingAngle = Vector3.zero;

        shootingAngle = -frontOfBarrel.transform.right;
        shootingAngle.y = Mathf.Abs(transform.rotation.y);

        Debug.Log(shootingAngle);
        return shootingAngle;
    }

    //Following for testing
}
