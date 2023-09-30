using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtefactManager : MonoBehaviour
{
    public string art_name;
    public string art_description;
    public Sprite art_icon;
    public Rarity rarity;

    public enum Rarity
    {
        normal,
        rare,
        epic,
        legendary
    }


    private int level = 1;
    private int max_level = 5;
    private bool unlocked=false;
    
    public void Unlock()
    {
        unlocked = true;
        gameObject.SetActive(true);
    }
    public void Upgrade()
    {
        if(level < max_level)
        {
            level++;
        }
    }
    public bool isLocked()
    {
        if (unlocked) return false;
        else return true;
    }
    public bool isMaxed()
    {
        if (level >= max_level) return true;
        else return false;
    }

    public Color GetRarityColor()
    {
        if(rarity == Rarity.normal) return Color.gray;
        else if (rarity == Rarity.rare) return Color.cyan;
        else if (rarity == Rarity.epic) return Color.magenta;
        else if (rarity == Rarity.legendary) return Color.red;

        return Color.white;
    }

    public int GetLevel()
    {
        return level;
    }
}
