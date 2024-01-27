using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownAmmoController : MonoBehaviour
{
    public string clownName = "";
    public int audioID = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        TestClownController.instance.loadAmmo(clownName, audioID);
        if(other.tag == "Load") Destroy(this.gameObject);
    }
}
