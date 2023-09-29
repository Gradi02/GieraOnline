using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public int hp = 20;
    public int mana = 0;
    private int speed = 8;
    private int damage = 8;
    private int damageMultiplier = 2;
    private int critChance = 50;
    private int critMultiplier = 3;
    private float modeCooldown = 3;
    private float gunCooldown = 0.8f;

    public int enemyKilledPerRound = 0;
    public int enemyKilled = 0;
    public Slider mana_slider;
    public Slider hp_slider;

    public TextMeshProUGUI hp_txt;
    public TextMeshProUGUI mana_txt;

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage()
    {
        damage += 1;
    }

    public int GetCritChance()
    {
        return critChance;
    }

    public void SetCritChance()
    {
        critChance += 1;
    }

    public int GetMultiplier()
    {
        return damageMultiplier;
    }

    public void SetMultiplier()
    {
        damageMultiplier += 1;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public void SetSpeed()
    {
        speed += 1;
    }

    public int GetCritMulti()
    {
        return critMultiplier;
    }

    public void SetCritMulti()
    {
        critMultiplier += 1;
    }

    public float GetModeCooldown()
    {
        return modeCooldown;
    }

    public float GetGunCooldown()
    {
        return gunCooldown;
    }

    private void Update()
    {
        mana_slider.value = mana;
        hp_slider.value = hp;

        hp_txt.text = "Health: " + hp.ToString();
        mana_txt.text = "Mana: " + mana.ToString();
    }
}
