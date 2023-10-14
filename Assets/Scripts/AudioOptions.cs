using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour
{
    private float volume_music = 0.5f;
    private float volume_sounds = 0.5f;
    private void Start()
    {
        if (PlayerPrefs.GetFloat("volume_music") == 0)
        {
            PlayerPrefs.SetFloat("volume_music", volume_music);
            PlayerPrefs.SetFloat("volume_sounds", volume_sounds);
        }
    }
}
