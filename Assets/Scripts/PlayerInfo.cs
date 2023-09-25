using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public int hp = 20;
    public int mana = 0;
    private int damage = 8;
    public Slider mana_slider;
    public Slider hp_slider;

    public TextMeshProUGUI hp_txt;
    public TextMeshProUGUI mana_txt;

    public float GetDamage()
    {
        return damage;
    }

    private void Update()
    {
        mana_slider.value = mana;
        hp_slider.value = hp;

        hp_txt.text = "Health: " + hp.ToString();
        mana_txt.text = "Mana: " + mana.ToString();
    }
}
