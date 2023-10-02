using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtefactManager : MonoBehaviour
{
    [Header("Opis")]
    public string[] art_description;

    [Header("inne")]
    public string art_name;
    public Sprite art_icon;


    private int level = 0;
    private int max_level = 5;
    private bool unlocked=false;
    
    public void Unlock()
    {
        unlocked = true;
        gameObject.SetActive(true);
        level++;
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

    public int GetLevel()
    {
        return level;
    }
}
