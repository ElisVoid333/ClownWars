/////////////////////////////////////////////////////////////////////////////////////////////////////
//
//   Project - CLOWN WARS
//   Filename - CastlePartControler.cs
//   Author - Eric DeMarbre
//   Date - January 27 2024
//
//   Description - Controls the behaviour of the castle parts for Clown Wars, particularly physics  
//   and collision behaviour. 
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class CastlePartController : MonoBehaviour
{
    public float killTime = 3.0f;
    public bool isHit = false;
    public bool couldExplode = false;
    public int score = 0;
    public GameObject particleSystem;

    private bool done = false;
    private bool destroying = false;
    private Rigidbody body;
    public float groundTimer = 0.0f;


    public AudioClip[] poofClips;
    private AudioSource castlePartAudio;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        castlePartAudio = GetComponent<AudioSource>();  
    }

    private void Update()
    {
        if (done && !destroying) {
            destroying = true;
            StartCoroutine(DestroyPiece());        
        }

        if (GetComponent<Rigidbody>().IsSleeping())
        {
            GetComponent<Rigidbody>().WakeUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Clown")
        {
            body.constraints = RigidbodyConstraints.None;
            isHit = true;
        }

        if (other.tag == "PlayArea")
        {
            done = true;
            StartCoroutine(DestroyPiece());
        }

        if (other.tag == "Explosion")
        {
            couldExplode = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Explosion")
        {
            couldExplode = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;

        if (hit.tag == "Castle")
        {
            if (hit.GetComponent<CastlePartController>().isHit)
            {
                body.constraints = RigidbodyConstraints.None;
                isHit = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject hit = collision.gameObject;

        if (hit.tag == "Ground" && isHit)
        {
            groundTimer += Time.deltaTime;

            if(groundTimer >= killTime)
            {
                done = true;
            }
        }
    }

    public void Reset()
    {
        couldExplode = false;
    }

    IEnumerator DestroyPiece()
    {
        Instantiate(particleSystem, this.transform.position, Quaternion.identity);
        castlePartAudio.PlayOneShot(poofClips[Random.Range(0, 3)]);
        GameController.instance.incremeantScore(score);
        while(castlePartAudio.isPlaying) yield return null;
        Destroy(this.gameObject);
    }
}
