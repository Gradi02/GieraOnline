using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spark : MonoBehaviour
{
    int bulletSpeed = 3;
    int damage = 1;
    public Color sparkyColor;

    private void Start()
    {
        Destroy(this.gameObject, 1);
    }
    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * transform.right;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyInfo>().Damage(damage, false, sparkyColor);
            Destroy(this.gameObject);
        }
    }
}
