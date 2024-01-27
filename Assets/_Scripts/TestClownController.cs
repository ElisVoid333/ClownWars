using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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
        if(Input.GetKeyDown(KeyCode.Space) && spaceUp)
        {
            spaceUp = false;
            LaunchClown();
        }

        if(Input.GetKeyUp(KeyCode.Space) && !spaceUp) spaceUp = true;   
    }

    private void LaunchClown()
    {
        if (ammoLoaded.Count > 0)
        {
            Ammo nextClown = ammoLoaded[0];

            switch (nextClown.ClownType)
            {
                case "Rocket Clown":
                    CurrentClown = Instantiate(RocketClownPrefab, this.transform);
                    break;
                case "Bomb Clown":
                    CurrentClown = Instantiate(BombClownPrefab, this.transform);
                    break;
                case "Normal Clown":
                default:
                    CurrentClown = Instantiate(NormalClownPrefab, this.transform);
                    break;
            }

            ammoLoaded.RemoveAt(0);

            Vector3 direction = LaunchTarget.transform.position - CurrentClown.transform.position;
            direction = Vector3.Normalize(direction);
            direction *= LaunchSpeed;

            CurrentClown.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
            CurrentClown.GetComponent<ClownStandinController>().launched = true;

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
}
