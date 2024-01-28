using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAmmoController : MonoBehaviour
{
    public GameObject clownRoot;
    public string clownName = "";
    public int audioID = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Load")
        {
            TestClownController.instance.loadAmmo(clownName, audioID);
            if (clownName == "Normal Clown") GameController.instance.SpawnNormalClown();
            if (clownName == "Rocket Clown") GameController.instance.SpawnRocketClown();
            Destroy(clownRoot);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Funnel")
        {
            clownRoot.GetComponent<ClownSoundController>().playSlideSound();
        }
    }
}
