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
        if (clownName == "Rocket Clown") GameController.instance.SpawnRocketClown(); /// JUST FOR TESTING
        if (other.tag == "Load")
        {
            if (clownName == "Normal Clown")
            {
                GameObject clownShell = this.transform.parent.gameObject;
                Destroy(clownShell);
            }

            if (clownName == "Rocket Clown")
            {
                GameObject clownShell = this.transform.parent.gameObject;
                clownShell = clownShell.transform.parent.gameObject;
                Destroy(clownShell);
            }
        }
        
    }
}
