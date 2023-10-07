using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySorting : MonoBehaviour
{
    private GameObject player;
    private GameObject wand;
    private GameObject art;
    private SpriteRenderer playerSR;
    private GameObject playerFeets;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wand = player.transform.GetChild(2).gameObject;
        art = player.transform.GetChild(1).gameObject;
        playerSR = player.GetComponent<SpriteRenderer>();
        playerFeets = player.transform.GetChild(0).gameObject;
    }
    void Update()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (enemy.transform.GetChild(0) != null)
            {
                int layer = Mathf.RoundToInt(enemy.transform.GetChild(0).position.y * 100);

                enemy.GetComponent<SpriteRenderer>().sortingOrder = -layer;

                if (enemy.transform.Find("armor"))
                {
                    enemy.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = -layer + 1;
                }
            }
            else
            {
                int layer = Mathf.RoundToInt(enemy.transform.position.y * 100);

                enemy.GetComponent<SpriteRenderer>().sortingOrder = -layer;

                if (enemy.transform.Find("armor"))
                {
                    enemy.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = -layer + 1;
                }
            }
        }

        playerSR.sortingOrder = -Mathf.RoundToInt(playerFeets.transform.position.y * 100);
        wand.GetComponent<SpriteRenderer>().sortingOrder = playerSR.sortingOrder + 2;
        
        for(int i=0; i<art.transform.childCount; i++)
        {
            if(art.transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                art.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = playerSR.sortingOrder + 1;
        }

        foreach(GameObject g in GameObject.FindGameObjectsWithTag("pop"))
        {
            int layer = Mathf.RoundToInt(g.transform.position.y * 100);

            g.GetComponent<UnityEngine.Rendering.SortingGroup>().sortingOrder = -layer;
        }


    }
}
