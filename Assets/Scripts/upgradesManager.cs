using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class upgradesManager : MonoBehaviour
{
    private int upgradeLevel = 0;
    private int maxlvl = 10;
    private Color max;
    public GameObject upgradeUI;
    public GameObject[] levels;
    public Color upgraded;
    private void Start()
    {
        max = Color.white;
        max.a = 0.5f;

        foreach (GameObject g in levels)
        {
            g.GetComponent<Image>().color = Color.gray;
        }
    }
    private void Update()
    {
        if(upgradeLevel >= maxlvl)
        {
            transform.GetChild(3).transform.GetComponent<Button>().interactable = false;
            transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = upgraded;
            transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Max";

            transform.GetChild(0).GetComponent<Image>().color = upgraded;
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = upgraded;
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = upgraded;
        }
        else if(!upgradeUI.GetComponent<upgrades_text>().CanUpgrade())
        {
            transform.GetChild(3).transform.GetComponent<Button>().interactable = false;
            transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = max;

            transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = max;
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = max;
        }
        else
        {
            transform.GetChild(3).transform.GetComponent<Button>().interactable = true;
            transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;

            transform.GetChild(0).GetComponent<Image>().color = Color.white;
            transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.white;
            transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }

    public void AddLevel()
    {
        if (upgradeLevel < maxlvl && upgradeUI.GetComponent<upgrades_text>().CanUpgrade())
        {
            FindObjectOfType<AudioManager>().Play("click");
            upgradeLevel++;
            levels[upgradeLevel-1].GetComponent<Image>().color = upgraded;
        }
    }
}
