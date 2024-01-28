using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;

public class ClownStandinController : MonoBehaviour
{
    public GameObject clownRoot;
    public string clownName = "";

    public bool exploding = false;
    public bool launched = false;
    public float killVelocity = 0.1f;
    public float killTime = 3.0f;
    private bool done = false;
    private bool exploded = false;

    public float explosionForce = 15.0f;
    public float explosionRadius = 3.0f;
    public float upwardsModifier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(done && GetComponent<Rigidbody>().velocity.magnitude <= killVelocity)
        {
            StartCoroutine(KillClown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (launched)
        {
            GameObject gameObject = collision.gameObject;

            if (gameObject.tag == "Castle" || gameObject.tag == "Ground")
            {
                if(!done) clownRoot.GetComponent<ClownSoundController>().playSlideSound();

                if (!exploding)
                {
                    done = true;
                } else
                {
                    if(!exploded) StartCoroutine(ExplodeClown());
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayArea")
        {
            Destroy(clownRoot);
        }
    }

    IEnumerator KillClown()
    {
        yield return new WaitForSeconds(killTime);
        Destroy(clownRoot);
    }

    IEnumerator ExplodeClown()
    {
       foreach(GameObject c in GameObject.FindGameObjectsWithTag("Castle"))
        {
            if(c.GetComponent<CastlePartController>().couldExplode)
            {
                c.GetComponent<CastlePartController>().isHit = true;
                c.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                c.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, this.transform.position, explosionRadius, upwardsModifier);
            }
        }

        
        // Activate Particle Effect
        done = true;
        exploded = true;

        yield return null;
    }
}
