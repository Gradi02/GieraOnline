using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class upgradesManager : MonoBehaviour
{
    private int upgradeLevel = 0;
    private int maxlvl = 20;
    private Color max;
    public GameObject upgradeUI;
    public GameObject[] levels;
    public Color upgraded;

    public Color normal2;
    public Color max2;
    public Color upgraded2;
    private bool max1 = false;
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
        isMaxed();

        if (!max1)
        {
            if (!upgradeUI.GetComponent<upgrades_text>().CanUpgrade())
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
        else
        {
            //transform.GetChild(3).transform.GetComponent<Button>() = upgraded2;
            if (upgradeLevel >= maxlvl)
            {
                transform.GetChild(3).transform.GetComponent<Button>().interactable = false;
                transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = upgraded2;
                transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Max";

                transform.GetChild(0).GetComponent<Image>().color = upgraded2;
                transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = upgraded2;
                transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = upgraded2;
            }
            else if (!upgradeUI.GetComponent<upgrades_text>().CanUpgrade())
            {
                transform.GetChild(3).transform.GetComponent<Button>().interactable = false;
                transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = max2;

                transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = max2;
                transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = max2;
            }
            else
            {
                transform.GetChild(3).transform.GetComponent<Button>().interactable = true;
                transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = normal2;

                transform.GetChild(0).GetComponent<Image>().color = normal2;
                transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = normal2;
                transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = normal2;
            }
        }
    }

    private void isMaxed()
    {
        if(upgradeLevel == 10 && !max1)
        {
            max1 = true;
            var colors = transform.GetChild(3).transform.GetComponent<Button>().colors;
            colors.highlightedColor = upgraded2;
            colors.pressedColor = Color.blue;
            transform.GetChild(3).transform.GetComponent<Button>().colors = colors;
        }
    }

    public void AddLevel()
    {
        if (upgradeLevel < maxlvl && upgradeUI.GetComponent<upgrades_text>().CanUpgrade())
        {
            if (upgradeLevel < 10)
            {
                FindObjectOfType<AudioManager>().Play("click");
                upgradeLevel++;
                levels[upgradeLevel - 1].GetComponent<Image>().color = upgraded;
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("click");
                upgradeLevel++;
                levels[upgradeLevel - 11].GetComponent<Image>().color = upgraded2;
            }
        }
    }
}
