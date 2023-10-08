using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtefactManager : MonoBehaviour
{
    public string art_name;
    public string art_description;
    public Sprite art_icon;
    

    private int level = 0;
    private int max_level = 5;
    private bool unlocked=false;
    public void Unlock()
    {
        unlocked = true;
        gameObject.SetActive(true);
        level++;

        if(art_name == "Sparky")
        {
            transform.root.gameObject.GetComponent<Shooting>().bulletPrefab.GetComponent<SpriteRenderer>().color = Color.yellow;
            transform.root.gameObject.GetComponent<Shooting>().bulletPrefab.GetComponent<TrailRenderer>().material = transform.root.gameObject.GetComponent<Shooting>().sparkyMat;
        }

        if(art_name == "Chain Bullet")
        {
            transform.root.gameObject.GetComponent<Shooting>().bulletPrefab.GetComponent<BulletsManager>().SetChain();
        }
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
