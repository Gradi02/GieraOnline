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
    private bool chain_hit = false;
    private List<Transform> closeEnemies = new List<Transform>();
    private int currentChain = 0;
    private int maxChain = 0;
    private float chainTimer = 0;
    private bool chain_hitted = false;
    private bool chainEffect = false;
    public Color chainColor;

    private float slowTime = 0;
    private float normalSpeed;
    private float stickyLevel = 0;

    [Header("Enemy Settings")]
    public bool canMove = true;
    private PlayerInfo info;



    public AudioSource source;
    public AudioClip death;
    public AudioClip prot1;
    public AudioClip prot2;
    public AudioClip prot3;
    void Start()
    {
        info = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        this.tag = "Enemy";
        rb = GetComponent<Rigidbody2D>();
        health *= waves.enemyHpMultiplier;
        speed += Random.Range(0.1f, 0.01f);
        normalSpeed = speed;
    }

    void Update()
    {
        if (chain_hit)
        {
            ChainEffects();
            canMove = false;
            rb.velocity = Vector2.zero;

            if (currentChain <= maxChain)
            {
                Transform nextTarget = FindClosestEnemy();

                if (nextTarget == null)
                {
                    currentChain = 0;
                    maxChain = 0;
                    chain_hit = false;
                    chainTimer = 0;
                }
                else if (Vector2.Distance(nextTarget.position, transform.position) <= 5)
                {
                    nextTarget.GetComponent<EnemyInfo>().SetChainHit(currentChain + 1, maxChain);
                    nextTarget.transform.Find("chain").GetComponent<ParticleSystem>().Play();
                    Damage(5, false, Color.blue);

                    currentChain = 0;
                    maxChain = 0;
                    chain_hit = false;
                    chainTimer = 0;
                }
                else
                {
                    currentChain = 0;
                    maxChain = 0;
                    chain_hit = false;
                    chainTimer = 0;
                }
            }
            else
            {
                currentChain = 0;
                maxChain = 0;
                chain_hit = false;
                chainTimer = 0;
            }
        }

        if (health <= 0 && !destroy)
        {
            // if(info.currentMana< info.GetMaxMana()) info.currentMana += 1;
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

            GetComponent<SpriteRenderer>().color = Color.black;
            destroy = true;

            if (transform.localScale.x <= 0.1f) Destroy(gameObject);
        }
        else 
        {

            if (poison && Time.time >= poison_cooldown)
            {
                poison_cooldown = Time.time + 3 + Random.Range(0.1f, 1f);

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
                    mutated_poison_cooldown = Time.time + 5 + Random.Range(0.1f, 1f);
                }
            }
            
            if(fast)
            {
                speed = info.GetSpeed() + 1f;
            }

            if(mutated_fast && Time.time >= fast_cooldown)
            {
                fast_cooldown = Time.time + attackSpeed + Random.Range(0.1f,1f);
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
                shooter_cooldown = Time.time + 2 + Random.Range(0.1f, 1f);

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
                    mutated_shooter_cooldown = Time.time + 3 + Random.Range(0.1f, 1f);
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
        
        if(chain_hitted)
        {
            chainTimer += Time.fixedDeltaTime;

            if(chainTimer >= 1)
            {
                chain_hitted = false;
                chainTimer = 0;
                chainEffect = false;
                GetComponent<SpriteRenderer>().color = Color.white;
                canMove = true;
            }
        }

        if(slowTime > 0)
        {
            slowTime -= Time.fixedDeltaTime;
            speed = normalSpeed / 2f;
        }
        else
        {
            speed = normalSpeed;
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

            int z = Random.Range(0, 3);
            if (z == 0) source.PlayOneShot(prot1);
            else if (z == 1) source.PlayOneShot(prot2); 
            else if (z == 2) source.PlayOneShot(prot3);
        }
        else
        {
            health -= damageDelta;
            source.PlayOneShot(death);
        }

        //PopUp
        DmgPopup.GetComponent<TextMeshPro>().text = damageDelta.ToString();
        DmgPopup.GetComponent<DmgPopup>().SetVelocity(12 * Time.deltaTime * transform.right);

        if (ifCrit)
        {
            DmgPopup.GetComponent<TextMeshPro>().color = Color.red;
            DmgPopup.GetComponent<TextMeshPro>().fontStyle = TMPro.FontStyles.Bold;
            DmgPopup.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        }
    }

    public void SetChainHit(int chainNum, int max)
    {
        chain_hit = true;
        chain_hitted = true;
        currentChain = chainNum;
        maxChain = max;
    }

    private void ChainEffects()
    {
        if(!chainEffect)
        {
            GetComponent<SpriteRenderer>().color = chainColor;
            
            chainEffect = true;
        }
    }

    public bool isDestroyed()
    {
        return destroy;
    }

    private Transform FindClosestEnemy()
    {
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector2.Distance(e.transform.position, this.transform.position) <= 8 && !e.GetComponent<EnemyInfo>().isDestroyed())
                closeEnemies.Add(e.transform);
        }

        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform enemy in closeEnemies)
        {
            if (enemy != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.position);

                if (distanceToEnemy < shortestDistance)
                {
                    if (!enemy.GetComponent<EnemyInfo>().chain_hitted && !enemy.GetComponent<EnemyInfo>().isDestroyed())
                    {
                        shortestDistance = distanceToEnemy;
                        closestEnemy = enemy;
                    }
                }
            }
        }

        return closestEnemy;
    }

    public void SetSlow(int level)
    {
        slowTime = 0.2f * level;
    }
}
