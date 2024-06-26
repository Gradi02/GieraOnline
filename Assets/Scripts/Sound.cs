using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range (0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public enum VolumeType
    {
        Sounds,
        Music
    }

    public VolumeType type;

    [Range(0.1f, 1f)]
    public float prioriti;
}
