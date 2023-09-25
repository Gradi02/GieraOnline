using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    public float bulletSpeed = 10;
    public ParticleSystem bulletparticle;
    private GameObject player;
    private int currentMode; // 1-Air > 2-Water > 3-Fire > 4-Nature
    private float damage;
    void Start()
    {
        Destroy(this.gameObject, 10);
        player = GameObject.FindGameObjectWithTag("Player");

        ParticleSystem ps = bulletparticle.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule col = bulletparticle.main;
        col.startColor = player.GetComponent<ChangeMode>().GetColor();
        damage = player.GetComponent<PlayerInfo>().GetDamage();
    }

    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * transform.right;
        currentMode = player.GetComponent<ChangeMode>().GetMode();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if(collision.gameObject.GetComponent<EnemyInfo>().type == EnemyInfo.types.Air)
            {
                if(currentMode == 1)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage;
                }
                else if(currentMode == 2)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage/2;
                }
                else if (currentMode == 3)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage;
                }
                else if (currentMode == 4)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage*2;
                }
            }
            else if (collision.gameObject.GetComponent<EnemyInfo>().type == EnemyInfo.types.Water)
            {
                if (currentMode == 1)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage*2;
                }
                else if (currentMode == 2)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage;
                }
                else if (currentMode == 3)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage/2;
                }
                else if (currentMode == 4)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage;
                }
            }
            else if (collision.gameObject.GetComponent<EnemyInfo>().type == EnemyInfo.types.Fire)
            {
                if (currentMode == 1)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage;
                }
                else if (currentMode == 2)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage*2;
                }
                else if (currentMode == 3)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage;
                }
                else if (currentMode == 4)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage/2;
                }
            }
            else if (collision.gameObject.GetComponent<EnemyInfo>().type == EnemyInfo.types.Nature)
            {
                if (currentMode == 1)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage/2;
                }
                else if (currentMode == 2)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage;
                }
                else if (currentMode == 3)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage*2;
                }
                else if (currentMode == 4)
                {
                    collision.gameObject.GetComponent<EnemyInfo>().health -= damage;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
