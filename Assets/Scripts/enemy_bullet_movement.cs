using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet_movement : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public float power;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * power;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
