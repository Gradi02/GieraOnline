using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float TimeToSpawn = 1;

    //tu wrzucamy prefaby wrog�w zwyk�ych
    [SerializeField] private GameObject[] enemys;
    //a tu zmutowanych tych du�ych
    [SerializeField] private GameObject[] megaEnemys;

    private SpriteRenderer spriteRenderer;
    private float timer = 0;
    private float startScale = 1;
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

        anim.a = Mathf.Sin(timer*4);
        spriteRenderer.color = anim;
    }

    private void SpawnEnemy()
    {
        int hard = Random.Range(1, 5);
        int level = 1;

        if (hard == 1) level = Random.Range(1, waves.currentEnemyLevel);
        int muted = Random.Range(1, 30);


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