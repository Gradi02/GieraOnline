using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic_knife : MonoBehaviour
{
    public GameObject knife;
    public GameObject anchor;
    private int level;

    private void Start()
    {

    }
    private void Update()
    {
        int level = GetComponent<ArtefactManager>().GetLevel();
    }

    public int Getlvl()
    {
        return level;
    }
}