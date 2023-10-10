using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet_movement : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float power;
    private Vector3 x;
    public int damage;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        x = new Vector3 (Random.Range(-1, 1), Random.Range(-2, 2),0);
        Vector3 direction = player.transform.position + x - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * power;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerInfo>().GetHitted(damage);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("barrier"))
        {
            Destroy(this.gameObject);
        }
    }
}
