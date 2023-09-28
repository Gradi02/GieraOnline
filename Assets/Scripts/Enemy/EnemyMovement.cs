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
            if (Vector2.Distance(player.transform.position, transform.position) > 6)
            {
                Vector3 dir = (player.transform.position - transform.position).normalized;
                GetComponent<Rigidbody2D>().velocity = dir * enemyInfo.speed;
            }
            else
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

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
