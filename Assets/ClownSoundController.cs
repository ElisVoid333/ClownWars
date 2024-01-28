using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ClownSoundController : MonoBehaviour
{
    public int audioID = 0;

    public AudioClip[] clownLaughs;
    public AudioClip[] clownGrabs;
    public AudioClip[] clownSlides;
    public AudioClip[] clownShots;
    public AudioClip[] clownHits;

    private AudioSource clownSoundSource;

    private float laughTime = 0.0f;
    private float lastLaugh = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        clownSoundSource = GetComponent<AudioSource>();
        laughTime = randomLaughTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastLaugh >= laughTime)
        {
            if (!clownSoundSource.isPlaying)
            {
                clownSoundSource.PlayOneShot(clownLaughs[randomTrack()]);
            }

            lastLaugh = Time.time;
            laughTime = randomLaughTime();
        }
    }

    public void playGrabSound()
    {
        if(clownSoundSource.isPlaying) clownSoundSource.Stop();

        clownSoundSource.PlayOneShot(clownGrabs[randomTrack()]);
    }

    private int randomTrack()
    {
        int random = Random.Range(0, 3);
        Debug.Log(random);
        return random + audioID;
    }

    private float randomLaughTime()
    {
        int random = Random.Range(1, 5);

        return 1.5f * random;
    }

    public void playShotSound()
    {
        if (clownSoundSource.isPlaying) clownSoundSource.Stop();

        clownSoundSource.PlayOneShot(clownShots[randomTrack()]);
    }

    public void playSlideSound()
    {
        if (clownSoundSource.isPlaying) clownSoundSource.Stop();

        clownSoundSource.PlayOneShot(clownSlides[randomTrack()]);
    }
}
