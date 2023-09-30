using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    private int maxhp = 20;
    public int currentHp = 1;
    private int maxmana = 20;
    public int currentMana = 1;
    private int speed = 8;
    private int damage = 8;
    private float damageMultiplier = 2;
    private int critChance = 5;
    private float critMultiplier = 3;
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

    public int GetMaxMana()
    {
        return maxmana;
    }

    public int GetMaxHp()
    {
        return maxhp;
    }

    public void SetMaxMana()
    {
        maxmana += 2;
    }

    public void SetMaxHP()
    {
        maxhp += 2;
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

    public float GetMultiplier()
    {
        return damageMultiplier;
    }

    public void SetMultiplier()
    {
        damageMultiplier += 0.5f;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public void SetSpeed()
    {
        speed += 1;
    }

    public float GetCritMulti()
    {
        return critMultiplier;
    }

    public void SetCritMulti()
    {
        critMultiplier += 0.5f;
    }

    public void SetGunCooldown()
    {
        gunCooldown -= 0.05f;
    }

    //public float GetModeCooldown()
    //{
    //    return modeCooldown;
    //}

    public float GetGunCooldown()
    {
        return gunCooldown;
    }

    public void SetStats()
    {
        hp_slider.maxValue = maxhp;
        mana_slider.maxValue = maxmana;
        currentHp = maxhp;
        currentMana = maxmana;
    }

    public void GetHitted(int dmg_in)
    {
        currentHp -= dmg_in;
    }

    private void Update()
    {
        mana_slider.value = currentMana;
        hp_slider.value = currentHp;

        hp_txt.text = "Health: " + currentHp.ToString();
        mana_txt.text = "Mana: " + currentMana.ToString();
    }
}
