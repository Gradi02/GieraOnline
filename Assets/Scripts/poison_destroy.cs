using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poison_destroy : MonoBehaviour
{

    private float delay;

    void Start()
    {
        delay = Time.time + 6;
    }
    void Update()
    {
        if (Time.time > delay)
        {
            transform.localScale -= new Vector3(Time.deltaTime, Time.deltaTime, Time.deltaTime);

            if (transform.localScale.x <= 0.1f) Destroy(gameObject);
            
        }
    }
}
