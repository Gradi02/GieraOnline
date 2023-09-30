using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float TimeToSpawn = 1;

    //tu wrzucamy prefaby wrogów zwyk³ych
    public GameObject[] enemys;
    //a tu zmutowanych tych du¿ych
    public GameObject[] megaEnemys;

    private SpriteRenderer spriteRenderer;
    private float timer = 0;
    private float startScale = 2;
    private Color anim;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = Color.white;
    }

    private void Update()
    {
        if(timer > TimeToSpawn)
        {
            SpawnEnemy();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        startScale -= Time.fixedDeltaTime / 2;
        transform.localScale = new Vector3(startScale, startScale, startScale);

        anim.a -= Time.fixedDeltaTime;
        spriteRenderer.color = anim;
    }

    private void SpawnEnemy()
    {
        int hard = Random.Range(1, 3);
        int level = 1;

        if (hard == 1) level = Random.Range(1, waves.currentEnemyLevel+1);
        int muted = Random.Range(1, 20);

        if (level == 1)
        {
            if(muted == 1) Instantiate(megaEnemys[level-1], transform.position, Quaternion.identity);
            else Instantiate(enemys[level - 1], transform.position, Quaternion.identity);
        }
        else if(level == 2)
        {
            if (muted == 1) Instantiate(megaEnemys[level - 1], transform.position, Quaternion.identity);
            else Instantiate(enemys[level - 1], transform.position, Quaternion.identity);
        }
        else if (level == 3)
        {
            if (muted == 1) Instantiate(megaEnemys[level - 1], transform.position, Quaternion.identity);
            else Instantiate(enemys[level - 1], transform.position, Quaternion.identity);
        }
        else if (level == 4)
        {
            if (muted == 1) Instantiate(megaEnemys[level - 1], transform.position, Quaternion.identity);
            else Instantiate(enemys[level - 1], transform.position, Quaternion.identity);
        }
        else if (level == 5)
        {
            if (muted == 1) Instantiate(megaEnemys[level - 1], transform.position, Quaternion.identity);
            else Instantiate(enemys[level - 1], transform.position, Quaternion.identity);
        }
    }
}
