using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Ammo
{
    public Ammo(string name, int audioTag)
    {
        string clownType = name;
        int clownAudioTag = audioTag;
    }
}

public class TestClownController : MonoBehaviour
{
    public static TestClownController instance = null;

    public GameObject ClownPrefab;
    public GameObject LaunchTarget;
    public float LaunchSpeed;

    private List<Ammo> ammoLoaded = new List<Ammo>();

    private GameObject CurrentClown = null;

    private bool spaceUp = true;
    private bool readyToLaunch = true;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && spaceUp && readyToLaunch)
        {
            spaceUp = false;
            LaunchClown();
        }

        if(Input.GetKeyUp(KeyCode.Space) && !spaceUp) spaceUp = true;   
    }

    public void SpawnClown()
    {
        StartCoroutine(ResetCastle());

        Ammo nextClown = ammoLoaded[0];


        CurrentClown = Instantiate(ClownPrefab, this.transform);
        readyToLaunch = true;
    }

    private void LaunchClown()
    {
        Vector3 direction = LaunchTarget.transform.position - CurrentClown.transform.position;
        direction = Vector3.Normalize(direction);
        direction *= LaunchSpeed;

        CurrentClown.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
        CurrentClown.GetComponent<ClownStandinController>().launched = true;
        readyToLaunch = false;
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
}
