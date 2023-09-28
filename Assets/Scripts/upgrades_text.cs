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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();    
    }

    void Update()
    {
        stats.text = "hp:" + player.hp.ToString() + 
                 "\r\nmana:"+ player.mana.ToString() + 
                 "\r\ndamage:" + player.GetDamage().ToString() + 
                 "\r\ncrit Chance:" + player.GetCritChance().ToString() +
                 "\r\ncrit Multiplier:" + player.GetCritMulti().ToString() +
                 "\r\ndamage Multiplier:" + player.GetMultiplier().ToString() +
                 "\r\nspeed:" + player.GetSpeed().ToString() + 
                 "\r\n\r\nEnemies killed:" + "tu powinna byc funkcja lol";
    }

    void upgradeHP()
    {

    }

    void upgradeMANA()
    {

    }

    void upgradeDAMAGE()
    {

    }

    void upgradeCRIT_CHANCE()
    {

    }

    void upgradeCRIT_MULTIPLIER()
    {

    }

    void upgradeDAMAGE_MULTIPLIER()
    {

    }

    void upgradeSPEED()
    {

    }
}
