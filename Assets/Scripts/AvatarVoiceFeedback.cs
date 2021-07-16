using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarVoiceFeedback : MonoBehaviour
{
    public AudioClip MaleLeft, MaleRight, FemaleLeft, FemaleRight, NeutralLeft, NeutralRight;

    private AudioSource AudioSource;

    
    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }


    public void PlayMaleLeft()
    {
        AudioSource.clip = MaleLeft;
        AudioSource.Play();
    }

    public void PlayMaleRight()
    {
        AudioSource.clip = MaleRight;
        AudioSource.Play();
    }

    public void PlayFemaleLeft()
    {
        AudioSource.clip = FemaleLeft;
        AudioSource.Play();
    }

    public void PlayFemaleRight()
    {
        AudioSource.clip = FemaleRight;
        AudioSource.Play();
    }

    public void PlayNeutralRight()
    {
        AudioSource.clip = NeutralRight;
        AudioSource.Play();
    }

    public void PlayNeutralLeft()
    {
        AudioSource.clip = NeutralLeft;
        AudioSource.Play();
    }

}
