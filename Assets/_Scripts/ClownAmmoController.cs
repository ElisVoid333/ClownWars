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
        if (clownName == "Rocket Clown") GameController.instance.SpawnNormalClown(); /// JUST FOR TESTING
        if (other.tag == "Load")
        {
            GameObject clownShell = this.transform.parent.gameObject;
            clownShell = clownShell.transform.parent.gameObject;
            Destroy(clownShell);
        }
        
    }
}
