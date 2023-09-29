using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySorting : MonoBehaviour
{
    private GameObject player;
    private GameObject wand;
    private SpriteRenderer playerSR;
    private GameObject playerFeets;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wand = player.transform.GetChild(1).gameObject;
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
            }
            else
            {
                int layer = Mathf.RoundToInt(enemy.transform.position.y * 100);

                enemy.GetComponent<SpriteRenderer>().sortingOrder = -layer;
            }
        }

        playerSR.sortingOrder = -Mathf.RoundToInt(playerFeets.transform.position.y * 100);
        wand.GetComponent<SpriteRenderer>().sortingOrder = playerSR.sortingOrder + 1;
    }
}
