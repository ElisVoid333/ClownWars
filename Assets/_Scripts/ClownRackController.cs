using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownRackController : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject clownSpawnPrefab;

    public int clownsToSpawn = 0;
    public int clownsSpawned = 0;
    public int maxClownsSpawned = 1;

    public AudioClip[] bingClips;
    private AudioSource rackSource;

    private void Awake()
    {
        rackSource = GetComponent<AudioSource>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(clownsSpawned < maxClownsSpawned && clownsToSpawn >= 1) // && gamecontroller clock is running 
        {
            clownsToSpawn--;
            clownsSpawned++;
            GameObject newClown = Instantiate(clownSpawnPrefab, spawnPoint.position, Quaternion.identity);
            rackSource.PlayOneShot(bingClips[Random.Range(0, 3)]);
            newClown.GetComponent<ClownSoundController>().audioID = Random.Range(0, 5);
        }
    }

    public void SpawnClown()
    {
        clownsToSpawn++;
    }

    public void ClownLoaded()
    {
        clownsSpawned--;
    }
}
