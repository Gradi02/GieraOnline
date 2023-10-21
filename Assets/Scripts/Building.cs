using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject[] ui;
    public GameObject buildingUI;
    void Start()
    {
        foreach(GameObject g in ui) g.SetActive(false);
    }

    public void BuildWand()
    {
        Destroy(buildingUI, 5);
        GetComponent<waves>().HideBuildingUI();
        GetComponent<waves>().WaveStart();
    }
}
