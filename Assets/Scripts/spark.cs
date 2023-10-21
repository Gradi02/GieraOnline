using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spark : MonoBehaviour
{
    int bulletSpeed = 3;
    int damage = 1;
    public Color sparkyColor;
   // private float duration = 0;

    private void Start()
    {
        Destroy(this.gameObject, 2);
    }
    void Update()
    {
        /* duration += Time.deltaTime;
         float Value = Mathf.Lerp(0.3f, 0, 1f * duration * 40);
         Debug.Log(transform.localScale);

         transform.localScale = new Vector3(Value, Value, Value);
         GetComponent<TrailRenderer>().textureScale = new Vector3(Value, Value, Value);
        */
        transform.position += bulletSpeed * Time.deltaTime * transform.right;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<EnemyInfo>().IsProtected()) Destroy(this.gameObject);

            collision.gameObject.GetComponent<EnemyInfo>().Damage(damage, false, sparkyColor);
            FindObjectOfType<AudioManager>().Play("slime dead");
            Destroy(this.gameObject);
        }
    }
}
