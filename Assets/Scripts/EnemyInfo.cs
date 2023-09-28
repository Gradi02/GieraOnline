using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health;
    [Min(1)] public float speed;
    public types type;
    public float damage;
    public float attackSpeed;

    private bool destroy = false;

    public enum types
    {
        Air,
        Water,
        Fire,
        Nature
    }

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
            if(info.mana<20) info.mana += 1;
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

    public void DestroyEnemy()
    {
        GetComponent<EnemyMovement>().enabled = false;
        transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        GetComponent<Animation>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.black;
        destroy = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
