using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poison_destroy : MonoBehaviour
{
    float timer = 0;
    void Update()
    {
        if(timer >= 5f)
            transform.localScale -= new Vector3(0.001f, 0.001f, 0);

        if(transform.localScale.x < 0)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PoisonPlayer"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>().poisonTime = 3;
        }
    }
}
