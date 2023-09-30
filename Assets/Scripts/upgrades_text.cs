using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class upgrades_text : MonoBehaviour
{
    public TextMeshProUGUI stats;
    public TextMeshProUGUI money;
    public TextMeshProUGUI pricetext;
    PlayerInfo player;
    public static int money_upgrade = 0;
    private int price = 1;
    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
    }

    void Update()
    {
        stats.text = "hp: " + player.GetMaxHp().ToString() + 
                 "\r\nmana: "+ player.GetMaxMana().ToString() + 
                 "\r\nspeed: " + player.GetSpeed().ToString() +
                 "\r\ndamage: " + player.GetDamage().ToString() + 
                 "\r\ndamage Multiplier: x" + player.GetMultiplier().ToString() +
                 "\r\ncrit Chance: " + player.GetCritChance().ToString() + "%" +
                 "\r\ncrit Multiplier: x" + player.GetCritMulti().ToString() +
                 "\r\nshot Cooldown: " + player.GetGunCooldown().ToString() + " s" +
                 "\r\n\r\nEnemies killed: " + player.enemyKilled;
        money.text = "points: " + money_upgrade;
        pricetext.text = "upgrade price: " + price;
    }

    public void UpgradeHP()
    {
        if (money_upgrade >= price)
        {
            player.SetMaxHP();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void UpgradeMANA()
    {
        if (money_upgrade >= price)
        {
            player.SetMaxMana();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void UpgradeDAMAGE()
    {
        if (money_upgrade >= price)
        {
            player.SetDamage();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void UpgradeCRIT_CHANCE()
    {
        if (money_upgrade >= price)
        {
            player.SetCritChance();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void UpgradeCRIT_MULTIPLIER()
    {
        if (money_upgrade >= price)
        {
            player.SetCritMulti();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void UpgradeDAMAGE_MULTIPLIER()
    {
        if (money_upgrade >= price)
        {
            player.SetMultiplier();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void UpgradeSPEED()
    {
        if (money_upgrade >= price)
        {
            player.SetSpeed();
            money_upgrade -= price;
            price += 1;
        }
    }

    public void UpgradeGUNCOOLDOWN()
    {
        if(money_upgrade >= price)
        {
            player.SetGunCooldown();
            money_upgrade -= price;
            price++;
        }
    }
}
