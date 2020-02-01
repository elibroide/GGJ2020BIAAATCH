using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicPlayer;
    public GameObject sourceGO;
    public AudioClip[] audioClips;
    private int index = 0;
    
    static Dictionary<string, AudioClip> soundsMap = new Dictionary<string, AudioClip>();
    static AudioSource[] sources;
    // Use this for initialization
    void Awake()
    {
        sources = sourceGO.GetComponents<AudioSource>();
        //sources = transform.Find("Sources").GetComponentInChildren<AudioSource>();
        foreach(AudioClip ac in audioClips)
        {
            soundsMap[ac.name] = ac;
        }

        musicPlayer.clip = soundsMap["music"];
        musicPlayer.Play();
    }

    //static public PlaySound(string name)
    //{

    //}
}
