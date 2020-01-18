using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip beepClip;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.clip = beepClip;
    }

    public void DoBeep()
    {
        audioSource.Play();
    }
}
