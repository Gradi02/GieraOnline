using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float health;
    [Min(1)] public float speed;
    public types type;

    public enum types
    {
        Air,
        Water,
        Fire,
        Nature
    }

    [Header("Enemy Settings")]
    public bool canMove = true;

    void Start()
    {
        this.tag = "Enemy";
    }

    void Update()
    {
        if(health <= 0) Destroy(gameObject);
    }
}
