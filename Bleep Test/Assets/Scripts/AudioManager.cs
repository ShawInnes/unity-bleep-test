using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip beepClip;
    public List<AudioClip> numberClips;
    public AudioSource audioSource;

    public void DoBeep()
    {
        audioSource.clip = beepClip;
        audioSource.Play();
    }

    public void DoLevel(int level)
    {
        if (level <= numberClips.Count)
        {
            audioSource.clip = numberClips[level - 1];
            audioSource.Play();
        }
    }
}
