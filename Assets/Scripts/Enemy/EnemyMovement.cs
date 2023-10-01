using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject playerFT;
    private GameObject spawnTransform;
    private EnemyInfo enemyInfo;
    private SpriteRenderer sr;
    private float end = 0;
    private float attackRange = 0.6f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerFT = player.transform.GetChild(0).gameObject;
        enemyInfo = GetComponent<EnemyInfo>();
        sr = GetComponent<SpriteRenderer>();
        
        try
        {
            spawnTransform = transform.Find("spawnTransform").gameObject;
        }
        catch
        {
            spawnTransform = null;
        }
    }

    void Update()
    {
        if ((player != null) && (enemyInfo != null) && (enemyInfo.canMove) && (!enemyInfo.isAttacking))
        {
            //movement
            if (Vector2.Distance(playerFT.transform.position, transform.position) > 0.5f)
            {
                Vector3 dir = (playerFT.transform.position - transform.position).normalized;
                GetComponent<Rigidbody2D>().velocity = dir * enemyInfo.speed;
            }
            else
            {
                Attack();
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

            //flipX
            Vector3 rot = playerFT.transform.position - transform.position;
            
            if(rot.x > 0)
            {
                sr.flipX = false;
                if (spawnTransform != null) spawnTransform.transform.localPosition = new Vector3(1.05f, 0.15f, 0);
            }
            else if(rot.x < 0)
            {
                sr.flipX = true;
                if (spawnTransform != null) spawnTransform.transform.localPosition = new Vector3(-1.05f, 0.15f, 0);
            }
        }


        if(enemyInfo.isAttacking && Time.time >= end)
        {
            enemyInfo.isAttacking = false;
            end = Time.time + enemyInfo.attackSpeed;

            if(Vector2.Distance(playerFT.transform.position, transform.position) <= attackRange)
                player.GetComponent<PlayerInfo>().GetHitted(enemyInfo.damage);
        }

        if(enemyInfo.run && !enemyInfo.hit)
        {
            if (Vector2.Distance(playerFT.transform.position, transform.position) <= 2)
            {
                player.GetComponent<PlayerInfo>().GetHitted(enemyInfo.damage * 2);
                enemyInfo.hit = true;
            }
        }
    }

    private void Attack()
    {
        enemyInfo.isAttacking = true;
    }
}
