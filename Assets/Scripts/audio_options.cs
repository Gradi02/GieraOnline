using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class audio_options : MonoBehaviour
{
    public TextMeshProUGUI music;
    public TextMeshProUGUI sounds;
    public TextMeshProUGUI monsters;

    public Slider music_slider;
    public Slider sounds_slider;
    public Slider monsters_slider;

    void Start()
    {
    
    }

    void Update()
    {
        music.text = music_slider.GetComponent<Slider>().value.ToString() + " %";
        sounds.text = sounds_slider.GetComponent<Slider>().value.ToString() + " %";
        monsters.text = monsters_slider.GetComponent<Slider>().value.ToString() + " %";
    }
}
