using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class upgrades_text : MonoBehaviour
{
    public TextMeshProUGUI stats;
    void Update()
    {
        stats.text = "dmg: NULL \n speed: NULL \n attack speed: NULL \n mana: NULL \n hp: NULL \n bullet size: NULL \n movement speed: NULL \n protection: NULL \n \n Enemies killed: NULL";
    }
}
