using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2move : MonoBehaviour
{
    private int damage = 10;
    public Color autoColor;

    void FixedUpdate()
    {
        transform.position += transform.right * Time.fixedDeltaTime * 20;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyInfo>().Damage(damage, false, autoColor);
            Destroy(this.gameObject);
        }
    }
}
