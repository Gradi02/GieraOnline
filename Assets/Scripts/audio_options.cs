using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class audio_options : MonoBehaviour
{
    public TextMeshProUGUI music;
    public TextMeshProUGUI sounds;

    public Slider music_slider;
    public Slider sounds_slider;

    void Update()
    {
        music.text = music_slider.GetComponent<Slider>().value.ToString() + " %";
        sounds.text = sounds_slider.GetComponent<Slider>().value.ToString() + " %";
    }
}
