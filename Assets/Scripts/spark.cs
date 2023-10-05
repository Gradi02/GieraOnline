using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spark : MonoBehaviour
{
    int bulletSpeed = 3;
    int damage = 1;

    private void Start()
    {
        float timer = Time.time + 1f;
        if(timer <= Time.time) Destroy(this.gameObject);
    }
    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * transform.right;
    }
}
