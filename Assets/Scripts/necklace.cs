using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class necklace : MonoBehaviour
{
    private float timeToHeal = 10;
    private float healTime = 10;
    void Update()
    {
        if (GetComponent<ArtefactManager>().GetLevel() == 1) { timeToHeal = 10; GetComponent<SpriteRenderer>().color = Color.white; }
        else if (GetComponent<ArtefactManager>().GetLevel() == 2) { timeToHeal = 9; GetComponent<SpriteRenderer>().color = Color.white; }
        else if (GetComponent<ArtefactManager>().GetLevel() == 3) { timeToHeal = 8; GetComponent<SpriteRenderer>().color = Color.white; }
        else if (GetComponent<ArtefactManager>().GetLevel() == 4) { timeToHeal = 7; GetComponent<SpriteRenderer>().color = Color.white; }
        else if (GetComponent<ArtefactManager>().GetLevel() == 5) { timeToHeal = 5; GetComponent<SpriteRenderer>().color = Color.yellow; }

        if (Time.time > healTime)
        {
            healTime = Time.time + timeToHeal;
            transform.root.GetComponent<PlayerInfo>().currentHp += 1;
        }   
    }
}
