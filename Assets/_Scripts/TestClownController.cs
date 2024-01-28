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
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;


public struct Ammo
{
    public string ClownType { get; }
    public int ClownAudioTag { get; }

    public Ammo(string name, int audioTag)
    {
        ClownType = name;
        ClownAudioTag = audioTag;
    }
}

public class TestClownController : MonoBehaviour
{
    public static TestClownController instance = null;

    public GameObject NormalClownPrefab;
    public GameObject RocketClownPrefab;
    public GameObject BombClownPrefab;

    public GameObject LaunchTarget;
    //public float LaunchSpeed;

    private List<Ammo> ammoLoaded = new List<Ammo>();

    public GameObject CurrentClown = null;

    private bool spaceUp = true;
    private bool readyToLaunch = true;

    private bool addingThrustingPower;                                  //Adds thrust force as player holds down button
    public float thrustForce;                                           //Force of objects flung out of cannon
    public float maxThrustForce;                                        //Maximum thrust to flung objects out of cannon
    public float minThrustForce;                                        //Minimum thrust to flung objects out of cannon

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Thrust Values Initialized
        maxThrustForce = 5000f;
        minThrustForce = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        /*-- Firing Canon --*/
        if (addingThrustingPower)
        {
            thrustForce += 1f;

            if (thrustForce >= maxThrustForce)
            {
                thrustForce = maxThrustForce;
            }
            //Debug.Log(thrustForce);
        }

        if (Input.GetKeyDown(KeyCode.Space) && spaceUp)
        {
            spaceUp = false;
            LaunchClown();
        }

        if(Input.GetKeyUp(KeyCode.Space) && !spaceUp) spaceUp = true;

    }

    public void LaunchClown()
    {
        if (ammoLoaded.Count > 0)
        {
            Ammo nextClown = ammoLoaded[0];

            switch (nextClown.ClownType)
            {
                case "Rocket Clown":
                    CurrentClown = Instantiate(RocketClownPrefab, LaunchTarget.transform.position, Quaternion.identity);
                    break;
                case "Bomb Clown":
                    CurrentClown = Instantiate(BombClownPrefab, LaunchTarget.transform.position, Quaternion.identity);
                    break;
                case "Normal Clown":
                default:
                    CurrentClown = Instantiate(NormalClownPrefab, LaunchTarget.transform.position, Quaternion.identity);
                    break;
            }

            ammoLoaded.RemoveAt(0);
            //Debug.Log("Thrust: " + thrustForce);
            GameObject clownCore = null;

            foreach (Transform child in CurrentClown.transform)
            {
                if (child.tag == "Clown")
                {
                    clownCore = child.gameObject;
                    //Debug.Log("Core: " + clownCore.name);
                    break;
                }else if(clownCore != null)
                {
                    //Debug.Log("Core: " + clownCore.name);
                    break;
                }

                foreach (Transform grandChild in child)
                {
                    if (grandChild.tag == "Clown")
                    {
                        clownCore = grandChild.gameObject;
                        //Debug.Log("Core: " + clownCore.name);
                        break;
                    }
                }
            }

            //Debug.Log("Core: " + clownCore.name);
            Debug.Log("Force: " + thrustForce);

            Vector3 direction = LaunchTarget.transform.position - this.transform.position;
            direction = Vector3.Normalize(direction);
            direction *= thrustForce;
            //Debug.Log("Direction: " + direction);

            clownCore.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
            clownCore.GetComponent<ClownStandinController>().launched = true;
            thrustForce = minThrustForce;   //Resets force applied to objects when shot

            StartCoroutine(ResetCastle());
        }
    }

    IEnumerator ResetCastle()
    {
        yield return new WaitForSeconds(5.0f);

        foreach (GameObject c in GameObject.FindGameObjectsWithTag("Castle"))
        {
            c.GetComponent<CastlePartController>().Reset();
        }
    }

    public void loadAmmo(string type, int audio)
    {
        Ammo newAmmo = new Ammo(type, audio);

        ammoLoaded.Add(newAmmo);
    }

    //Allow to add power to cannon shot
    public void addThrust(bool _addThrust)
    {
        //Debug.Log("Thrusting");
        addingThrustingPower = _addThrust;
    }
}
