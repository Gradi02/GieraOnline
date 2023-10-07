using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonBullet : MonoBehaviour
{
    public GameObject poison_pool;
    private float lifeTime = 10;
    public void SetVelocity(float lifeTime_in)
    {
        lifeTime = Time.time + lifeTime_in;
    }

    private void Update()
    {
        transform.position += Time.deltaTime * transform.right * 10;

        if(Time.time > lifeTime)
        {
            Instantiate(poison_pool, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
