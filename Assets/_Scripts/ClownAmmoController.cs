using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAmmoController : MonoBehaviour
{
    public string clownName = "";
    public int audioID = 0;

    private void OnTriggerEnter(Collider other)
    {
        TestClownController.instance.loadAmmo(clownName, audioID);
        if (clownName == "Normal Clown") GameController.instance.SpawnNormalClown();
        if(other.tag == "Load") Destroy(this.gameObject);
    }
}
