using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class upgrades_text : MonoBehaviour
{
    public TextMeshProUGUI stats;
    PlayerInfo player;
    public TextMeshProUGUI money;
    public int money_upgrade = 1000;
    private int price = 1;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();    
    }

    void Update()
    {
        stats.text = "hp: " + player.hp.ToString() + 
                 "\r\nmana: "+ player.mana.ToString() + 
                 "\r\ndamage: " + player.GetDamage().ToString() + 
                 "\r\ncrit Chance: " + player.GetCritChance().ToString() +
                 "\r\ncrit Multiplier: " + player.GetCritMulti().ToString() +
                 "\r\ndamage Multiplier: " + player.GetMultiplier().ToString() +
                 "\r\nspeed: " + player.GetSpeed().ToString() + 
                 "\r\n\r\nEnemies killed: " + player.enemyKilled;
    }

    public void upgradeHP()
    {
        if (money_upgrade >= price)
        {
            player.hp += 1;
            money_upgrade -= price;
            price += 1;
        }
    }

    public void upgradeMANA()
    {
        if (money_upgrade >= price)
        {
            player.mana += 1;
            money_upgrade -= price;
            price += 1;
            Debug.Log(player.mana);
        }
    }

    public void upgradeDAMAGE()
    {
        if (money_upgrade >= price)
        {
            player.SetDamage();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void upgradeCRIT_CHANCE()
    {
        if (money_upgrade >= price)
        {
            player.SetCritChance();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void upgradeCRIT_MULTIPLIER()
    {
        if (money_upgrade >= price)
        {
            player.SetCritMulti();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void upgradeDAMAGE_MULTIPLIER()
    {
        if (money_upgrade >= price)
        {
            player.SetMultiplier();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void upgradeSPEED()
    {
       if (money_upgrade >= price)
        {
            player.SetSpeed();
            money_upgrade -= price;
            price += 1;
        }
    }
}
