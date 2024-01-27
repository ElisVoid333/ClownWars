using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownRackController : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject clownSpawnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnClown()
    {
        Instantiate(clownSpawnPrefab, spawnPoint.position, Quaternion.identity);
    }
}
