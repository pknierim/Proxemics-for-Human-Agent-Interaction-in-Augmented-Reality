using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{
    public AudioClip AudioClip1;
    public AudioClip AudioClip2;

    private AudioSource AudioSource;

    
    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }


    public void PlayHallo()
    {
        Debug.Log("Play Audio file");

        // Debug.Log(AudioClip1);

        if (Random.value > 0.5) {
            AudioSource.clip = AudioClip1;
            Debug.Log("Play Audio 1");
            AudioSource.Play();

        }

        else
        {
            AudioSource.clip = AudioClip2;
            Debug.Log("Play Audio 2");
            AudioSource.Play();

        }

    }
}
