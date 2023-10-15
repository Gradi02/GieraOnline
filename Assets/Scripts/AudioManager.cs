using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.volume = 0.5f;
        }
    }

    private void Start()
    {
        Play("music");
    }

    private void Update()
    {
        foreach (Sound s in sounds)
        {
            if (s.type == Sound.VolumeType.Sounds)
            {
                s.source.volume = PlayerPrefs.GetFloat("volume_sounds") * s.prioriti;
            }
            else if (s.type == Sound.VolumeType.Music)
            {
                s.source.volume = PlayerPrefs.GetFloat("volume_music") * s.prioriti;
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
    
        if(s == null)
        {
            Debug.LogWarning("Nie ma takiej muzyczki!!!");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Nie ma takiej muzyczki!!!");
            return;
        }

        s.source.Stop();
    }

    public void SetPriority(string name, float new_priority)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Nie ma takiej muzyczki!!!");
            return;
        }

        s.prioriti = new_priority;
    }
}
