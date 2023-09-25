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
        if (health <= 0)
        {
            if(info.mana<20) info.mana += 1;
            Destroy(gameObject);
        }

       
    }
}
