using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class necklace : MonoBehaviour
{
    private float timeToHeal = 10;
    private float healTime = 10;
    void Update()
    {
        if(Time.time > healTime)
        {
            healTime = Time.time + timeToHeal;
            transform.root.GetComponent<PlayerInfo>().currentHp += GetComponent<ArtefactManager>().GetLevel();
        }      
    }
}
