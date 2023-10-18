using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class knife : MonoBehaviour
{
    public GameObject anchor;
    private float orbitSpeed = 100f;
    private float orbitRadius = 3f;

    public float currentAngle = 0;

    //public magic_knife magic_Knife;
    //public magic_knife angle;
    private void Start()
    {
        currentAngle = transform.parent.GetComponent<magic_knife>().angle;
    }
    private void Update()
    {
        if (anchor != null)
        {
            currentAngle += orbitSpeed * Time.deltaTime;
            currentAngle = currentAngle % 360;

            Vector2 offset = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad)) * orbitRadius;

            transform.position = anchor.transform.position + new Vector3(offset.x, offset.y, 0);

            Vector3 direction = anchor.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 45f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyInfo>().KnifeHit(transform.parent.GetComponent<magic_knife>().GetDamage(), this.gameObject);
        }
    }
}
