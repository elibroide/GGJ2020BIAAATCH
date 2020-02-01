using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicPlayer;
    public GameObject sourceGO;
    public AudioSource sourceWalk;
    public AudioClip[] audioClips;
    private static int index = 0;
    private static AudioSource _sourceWalk;
    
    static Dictionary<string, AudioClip> soundsMap = new Dictionary<string, AudioClip>();
    private static AudioSource[] sources;
    // Use this for initialization

    void Awake()
    {
        sources = sourceGO.GetComponents<AudioSource>();
        //sources = transform.Find("Sources").GetComponentInChildren<AudioSource>();
        foreach(AudioClip ac in audioClips)
        {
            soundsMap[ac.name] = ac;
        }
        _sourceWalk = sourceWalk;
    }

    internal static void Walk()
    {
        if (!_sourceWalk.isPlaying)
        {
            _sourceWalk.Play();
        }
        
    }

    static public void PlaySound(string name)
    {
        sources[index].clip = soundsMap[name];
        sources[index].Play();
        index++;
        if (index == sources.Length) index = 0;
    }

    internal static void StopWalk()
    {
        _sourceWalk.Stop();
    }
}
