using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    public GameObject enemy_basic;
    public Vector3 temp_shooter_cords;

    [Header("Enemy Stats")]
    public float health;
    [Min(1)] public float speed;
    public int damage;
    public float attackSpeed;

    [Header("TYPE")]
    public bool shooter;
    public bool poison;

    [Header("MUTATIONS")]
    public bool mutated_basic;
    public bool mutated_speed;
    public bool mutated_shooter;
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

        if(destroy)
        {
            transform.localScale -= new Vector3(Time.deltaTime,Time.deltaTime,Time.deltaTime);
            float obecnyKat = transform.rotation.eulerAngles.z;
            float nowyKat = obecnyKat + 90 * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, nowyKat);

            if (transform.localScale.x <= 0.1f) Destroy(gameObject);
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
    
    public void Shooter_skill() 
    {
        
    
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

    IEnumerator Shooter_cooldown()
    {
        yield return new WaitForSeconds(2f);
        temp_shooter_cords = new Vector3(info.transform.position.x, info.transform.position.y, 0);
    }
}
