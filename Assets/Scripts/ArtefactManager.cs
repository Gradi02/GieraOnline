using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
