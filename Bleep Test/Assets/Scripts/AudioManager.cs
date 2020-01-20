using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip beepClip;
    public AudioClip numbersClip;
    public AudioSource beepAudioSource;
    public AudioSource numbersAudioSource;

    private const float BeepLength = 0.5f;

    private readonly List<Tuple<int, float, float>> _numbersRanges = new List<Tuple<int, float, float>>();

    private void Awake()
    {
        _numbersRanges.Add(new Tuple<int, float, float>(1, 0.000f, 0.750f));
        _numbersRanges.Add(new Tuple<int, float, float>(2, 1.000f, 1.840f));
        _numbersRanges.Add(new Tuple<int, float, float>(3, 2.120f, 3.000f));
        _numbersRanges.Add(new Tuple<int, float, float>(4, 3.250f, 4.120f));
        _numbersRanges.Add(new Tuple<int, float, float>(5, 4.400f, 5.250f));
        _numbersRanges.Add(new Tuple<int, float, float>(6, 5.625f, 6.500f));
        _numbersRanges.Add(new Tuple<int, float, float>(7, 6.750f, 7.600f));
        _numbersRanges.Add(new Tuple<int, float, float>(8, 7.920f, 8.800f));
        _numbersRanges.Add(new Tuple<int, float, float>(9, 9.120f, 9.880f));
        _numbersRanges.Add(new Tuple<int, float, float>(10, 10.200f, 10.920f));
        _numbersRanges.Add(new Tuple<int, float, float>(11, 11.320f, 12.120f));
        _numbersRanges.Add(new Tuple<int, float, float>(12, 12.500f, 13.320f));
        _numbersRanges.Add(new Tuple<int, float, float>(13, 13.800f, 14.800f));
        _numbersRanges.Add(new Tuple<int, float, float>(14, 15.160f, 16.160f));
        _numbersRanges.Add(new Tuple<int, float, float>(15, 16.500f, 17.560f));
        _numbersRanges.Add(new Tuple<int, float, float>(16, 17.920f, 18.920f));
        _numbersRanges.Add(new Tuple<int, float, float>(17, 19.260f, 20.320f));
        _numbersRanges.Add(new Tuple<int, float, float>(18, 20.660f, 21.640f));
        _numbersRanges.Add(new Tuple<int, float, float>(19, 22.000f, 23.000f));
        _numbersRanges.Add(new Tuple<int, float, float>(20, 23.380f, 24.200f));
        _numbersRanges.Add(new Tuple<int, float, float>(21, 24.580f, 25.680f));
    }

    public void DoBeep()
    {
        beepAudioSource.clip = beepClip;
        beepAudioSource.Play();
    }

    public void DoLevel(int level)
    {
        var numbersItem = _numbersRanges.SingleOrDefault(p => p.Item1 == level);
        if (numbersItem != null)
        {
            numbersAudioSource.clip = numbersClip;
            numbersAudioSource.time = numbersItem.Item2;
            numbersAudioSource.PlayScheduled(AudioSettings.dspTime + BeepLength);
            numbersAudioSource.SetScheduledEndTime(AudioSettings.dspTime + BeepLength + (numbersItem.Item3 - numbersItem.Item2));
        }
    }
}
