using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using TMPro;

public class EnemyInfo : MonoBehaviour
{
    public GameObject enemy_basic;
    public Vector3 temp_shooter_cords;

    //poison
    public GameObject poison_pool;
    public GameObject poison_bullet;
    private float poison_cooldown = 1;
    private float mutated_poison_cooldown = 0;
    private float mutated_poison_burst = 0;

    //shooter
    public GameObject bullet;
    private float shooter_cooldown = 1;
    private float mutated_shooter_cooldown = 0;
    private float mutated_shooter_burst = 0;
    private int burst = 0;

    //speed
    private float fast_cooldown = 3;
    private bool speedUp = false;
    private float fast_timer = 0;
    private float speedTime = 0;
    public bool run = false;
    public bool hit = false;

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
    public bool mutated_armored;
    public bool mutated_shooter;
    public bool mutated_fast;
    public bool mutated_poison;

    [Header("Others")]
    [SerializeField] private GameObject particle;
    private Rigidbody2D rb;
    private bool destroy = false;
    [HideInInspector] public bool isAttacking = false;
    [SerializeField] private GameObject pfDamagePopup;

    [Header("Enemy Settings")]
    public bool canMove = true;
    private PlayerInfo info;

    void Start()
    {
        info = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        this.tag = "Enemy";
        rb = GetComponent<Rigidbody2D>();
        health *= waves.enemyHpMultiplier;
        speed += Random.Range(0.1f, 0.01f);
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
                        /*
                        int x = Random.Range(-7, 7);
                        int y = Random.Range(-7, 7);
                        Vector3 pois_cord = new Vector3(this.transform.position.x + x, this.transform.position.y + y, 0);
                        Instantiate(poison_pool, pois_cord, Quaternion.identity);
                        */

                        float randomAngel = Random.Range(0f, 360f);
                        float randomDistance = Random.Range(0.2f, 1.5f);
                        GameObject newB = Instantiate(poison_bullet, this.transform.position, Quaternion.Euler(0.0f, 0.0f, randomAngel));
                        newB.GetComponent<poisonBullet>().SetVelocity(randomDistance);

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
            
            if(fast)
            {
                speed = info.GetSpeed() + 0.5f;
            }

            if(mutated_fast && Time.time >= fast_cooldown)
            {
                fast_cooldown = Time.time + attackSpeed;
                canMove = false;
                rb.velocity = Vector3.zero;
                speedUp = true;
            }

            if(mutated_fast)
            {
                if(fast_timer > 1)
                {
                    speedUp = false;
                    fast_timer = 0;

                    Vector3 targetPos = (info.transform.position - transform.position).normalized;
                    rb.velocity = new Vector2(targetPos.x, targetPos.y).normalized * 20;
                    run = true;
                }

                if(speedTime > 2)
                {
                    rb.velocity = Vector3.zero;
                    run = false;
                    speedTime = 0;
                    canMove = true;
                    hit = false;
                }
            }

            if (shooter && Time.time >= shooter_cooldown)
            {
                shooter_cooldown = Time.time + 2;

                Instantiate(bullet, transform.GetChild(1).transform.position, Quaternion.identity);
            }

            if (mutated_shooter && Time.time >= mutated_shooter_cooldown)
            {
                if (burst < 5)
                {
                    if (Time.time >= mutated_shooter_burst)
                    {
                        mutated_shooter_burst = Time.time + 0.3f;
                        Instantiate(bullet, transform.GetChild(1).transform.position, Quaternion.identity);
                        burst++;
                    }
                }
                else
                {
                    burst = 0;
                    mutated_shooter_cooldown = Time.time + 3;
                }
            }

            if(protection <= 0 && (armored || mutated_armored))
            {
                if (transform.Find("armor"))
                {
                    transform.GetChild(1).GetComponent<SpriteRenderer>().color = Color.black;
                    transform.GetChild(1).transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);
                    float obecnyKat = transform.GetChild(1).transform.rotation.eulerAngles.z;
                    float nowyKat = obecnyKat + 90 * Time.deltaTime;
                    transform.GetChild(1).transform.rotation = Quaternion.Euler(0, 0, nowyKat);

                    if (transform.GetChild(1).transform.localScale.x <= 0.1f) Destroy(transform.GetChild(1).gameObject);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (speedUp)
        {
            fast_timer += Time.fixedDeltaTime;
        }

        if(run)
        {
            speedTime += Time.fixedDeltaTime;
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

    public void Damage(float damageDelta, bool ifCrit, Color normalColor)
    {
        //dmgpopup
        GameObject DmgPopup = Instantiate(pfDamagePopup, transform.position, Quaternion.identity);
        DmgPopup.GetComponent<TextMeshPro>().color = normalColor;
        
        //Zadawanie Damage
        if (protection > 0)
        {
            ifCrit = false;
            damageDelta = 1;
            DmgPopup.GetComponent<TextMeshPro>().color = Color.grey;
            protection -= 1;
        }
        else
        {
            health -= damageDelta;
        }

        //PopUp
        DmgPopup.GetComponent<TextMeshPro>().text = damageDelta.ToString();
        DmgPopup.GetComponent<DmgPopup>().SetVelocity(12 * Time.deltaTime * transform.right);

        /*
        if (damageDelta > (info.GetDamage() * (info.GetMultiplier() - (0.25f * info.GetMultiplier()))))
        {
            DmgPopup.GetComponent<TextMeshPro>().color = Color.yellow;
        }*/

        if (ifCrit)
        {
            DmgPopup.GetComponent<TextMeshPro>().color = Color.red;
            DmgPopup.GetComponent<TextMeshPro>().fontStyle = TMPro.FontStyles.Bold;
            DmgPopup.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        }
    }
}
