using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public GameObject enemy_basic;
    public Vector3 temp_shooter_cords;

    //poison
    public GameObject poison_pool;
    private float poison_cooldown = 1;
    private float mutated_poison_cooldown = 0;
    private float mutated_poison_burst = 0;
    private int burst = 0;

    //shooter
    public GameObject bullet;
    private float shooter_cooldown = 1;
    private float mutated_shooter_cooldown = 0;
    private float mutated_shooter_burst = 0;

    //speed
    private float fast_cooldown = 0;
    private bool speedup;
    private float speedup_cooldown = 0;

    [Header("Enemy Stats")]
    public float health;
    [Min(1)] public float speed;
    public int damage;
    public float attackSpeed;
    public int protection;

    [Header("TYPE")]
    public bool armored;
    public bool shooter;
    public bool fast;
    public bool poison;

    [Header("MUTATIONS")]
    public bool mutated_basic;
    public bool motated_armored;
    public bool mutated_shooter;
    public bool mutated_fast;
    public bool mutated_poison;


    [Header("Others")]
    [SerializeField] private GameObject particle;
    private bool destroy = false;
    [HideInInspector] public bool isAttacking = false;

    [Header("Enemy Settings")]
    public bool canMove = true;
    private PlayerInfo info;

    void Start()
    {
        info = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        this.tag = "Enemy";
        health *= waves.enemyHpMultiplier;
    }

    void Update()
    {

        if (health <= 0 && !destroy)
        {
            if(info.currentMana< info.GetMaxMana()) info.currentMana += 1;
            info.enemyKilled++;
            info.enemyKilledPerRound++;
            if(mutated_basic) Mutated_basic_skill();
            DestroyEnemy();
        }

        if (destroy)
        {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
            float obecnyKat = transform.rotation.eulerAngles.z;
            float nowyKat = obecnyKat + 90 * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, nowyKat);

            if (transform.localScale.x <= 0.1f) Destroy(gameObject);
        }
        else 
        {

            if (poison && Time.time >= poison_cooldown)
            {
                poison_cooldown = Time.time + 3;

                Instantiate(poison_pool, this.transform.position, Quaternion.identity);
            }

            if (mutated_poison && Time.time >= mutated_poison_cooldown)
            {
                if (burst < 5)
                {
                    if (Time.time >= mutated_poison_burst)
                    {
                        int x = Random.Range(-7, 7);
                        int y = Random.Range(-7, 7);
                        Vector3 pois_cord = new Vector3(this.transform.position.x + x, this.transform.position.y + y, 0);
                        Instantiate(poison_pool, pois_cord, Quaternion.identity);
                        mutated_poison_burst = Time.time + 0.2f;
                        burst++;
                    }
                }
                else
                {
                    burst = 0;
                    mutated_poison_cooldown = Time.time + 5;
                }
            }

      /*      if(fast && Time.time >= fast_cooldown) 
            {
                speedup = true;
                
            }
            if (fast && speedup)
            { 
                speedup_cooldown = Time.time + 3;
                speed = 10;
                if (Time.time >= speedup_cooldown) speedup = false;
            }
            if (fast && !speedup)
            {
                speed = 2;
                fast_cooldown = Time.time + 6;
            }
      */


                if (shooter && Time.time >= shooter_cooldown)
            {
                shooter_cooldown = Time.time + 2;

                Instantiate(bullet, this.transform.position, Quaternion.identity);
            }

            if (mutated_shooter && Time.time >= mutated_shooter_cooldown)
            {
                if (burst < 10)
                {
                    if (Time.time >= mutated_shooter_burst)
                    {
                        mutated_shooter_burst = Time.time + 0.3f;
                        Instantiate(bullet, this.transform.position, Quaternion.identity);
                        burst++;
                    }
                }
                else
                {
                    burst = 0;
                    mutated_shooter_cooldown = Time.time + 5;
                }
            }


        }
    }

    public void Mutated_basic_skill()
    {
        Vector3 skillCords1 = new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, 0);
        Vector3 skillCords2 = new Vector3(this.transform.position.x + 1, this.transform.position.y - 1, 0);
        Vector3 skillCords3 = new Vector3(this.transform.position.x - 1, this.transform.position.y + 1, 0);
        Vector3 skillCords4 = new Vector3(this.transform.position.x - 1, this.transform.position.y - 1, 0);
        Instantiate(enemy_basic, skillCords1, Quaternion.identity);
        Instantiate(enemy_basic, skillCords2, Quaternion.identity);
        Instantiate(enemy_basic, skillCords3, Quaternion.identity);
        Instantiate(enemy_basic, skillCords4, Quaternion.identity);
    }

    public void DestroyEnemy()
    {
        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<Animation>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.black;
        destroy = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Instantiate(particle, transform.position, transform.rotation);
    }
}
