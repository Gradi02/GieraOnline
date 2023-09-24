using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private EnemyInfo enemyInfo;
    private SpriteRenderer sr;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyInfo = GetComponent<EnemyInfo>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if ((player != null) && (enemyInfo != null) && (enemyInfo.canMove))
        {
            //movement
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, enemyInfo.speed * Time.deltaTime);

            //flipX
            Vector3 rot = player.transform.position - transform.position;
            
            if(rot.x > 0)
            {
                sr.flipX = false;
            }
            else if(rot.x < 0)
            {
                sr.flipX = true;
            }
        }
    }
}
