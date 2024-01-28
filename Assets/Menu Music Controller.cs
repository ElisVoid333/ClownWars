using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    public AudioClip introClip;
    public AudioClip loop1Clip;
    public AudioClip loop2Clip;

    private AudioSource menuMusicSource;


    private void Awake()
    {
        menuMusicSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!menuMusicSource.isPlaying)
        {
            if(menuMusicSource.clip == introClip || menuMusicSource.clip == loop2Clip)
            {
                menuMusicSource.clip = loop1Clip;

            } else if (menuMusicSource.clip == loop1Clip)
            {
                menuMusicSource.clip = loop2Clip;
            }

            menuMusicSource.Play();
        }
    }
}
