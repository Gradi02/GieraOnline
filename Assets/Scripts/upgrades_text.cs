using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class upgrades_text : MonoBehaviour
{
    public TextMeshProUGUI money;
    public TextMeshProUGUI pricetext;
    PlayerInfo player;
    public static int money_upgrade = 0;
    private int price = 1;
    public TextMeshProUGUI start;

    [Header("Upgrades")]
    public TextMeshProUGUI healthValue;
    public TextMeshProUGUI speedValue;
    public TextMeshProUGUI damageValue;
    public TextMeshProUGUI dmgMultiValue;
    public TextMeshProUGUI critValue;
    public TextMeshProUGUI critMultiValue;
    public TextMeshProUGUI gunValue;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
    }

    void Update()
    {
        healthValue.text = player.GetMaxHp().ToString() + " hp";
        speedValue.text = player.GetSpeed().ToString();
        damageValue.text = player.GetDamage().ToString();
        dmgMultiValue.text = "x" + player.GetMultiplier().ToString();
        critValue.text = player.GetCritChance().ToString() + "%";
        critMultiValue.text = "x" + player.GetCritMulti().ToString();
        gunValue.text = player.GetGunCooldown().ToString() + " s";

        money.text = "points: " + money_upgrade;
        pricetext.text = "upgrade price: " + price;

        start.text = "start wave " + (waves.wave + 1);

        if(money_upgrade < price)
        {
            pricetext.color = Color.gray;
        }
        else
        {
            pricetext.color = Color.white;
        }
    }

    public bool CanUpgrade()
    {
        if (money_upgrade >= price) return true;
        return false;
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
