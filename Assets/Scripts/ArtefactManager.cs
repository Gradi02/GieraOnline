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
        FindObjectOfType<AudioManager>().Play("click");
        unlocked = true;
        gameObject.SetActive(true);
        level++;

        if(art_name == "Sparky")
        {
            transform.root.gameObject.GetComponent<Shooting>().bulletPrefab.GetComponent<SpriteRenderer>().color = Color.yellow;
            transform.root.gameObject.GetComponent<Shooting>().bulletPrefab.GetComponent<TrailRenderer>().material = transform.root.gameObject.GetComponent<Shooting>().sparkyMat;
        }

        if(art_name == "Perfect Bullet")
        {
            transform.root.gameObject.GetComponent<Shooting>().SetChain();
        }

        if(art_name == "Death Note")
        {
            transform.root.gameObject.GetComponent<Shooting>().SetBook();
        }
    }
    public void Upgrade()
    {
        if(level < max_level)
        {
            FindObjectOfType<AudioManager>().Play("click");
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
