using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class ClownAmmoController : MonoBehaviour
{
    public GameObject clownRoot;
    public string clownName = "";
    public int audioID = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Load")
        {
            TestClownController.instance.loadAmmo(clownName, clownRoot.GetComponent<ClownSoundController>().audioID);
            GameController.instance.LoadClown(clownName);
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

        if(collision.gameObject.tag == "Ground")
        {
            GameController.instance.LoadClown(clownName);
            Destroy(clownRoot);
        }
    }
}
