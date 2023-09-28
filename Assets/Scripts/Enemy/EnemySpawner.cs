using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float TimeToSpawn = 1;

    public GameObject[] Airtypes;
    public GameObject[] Watertypes;
    public GameObject[] Firetypes;
    public GameObject[] Naturetypes;

    private EnemyInfo.types enemyType;
    private SpriteRenderer spriteRenderer;
    private float timer = 0;
    private float startScale = 1;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RandomEnemyType();
    }

    private void RandomEnemyType()
    {
        int type = Random.Range(0, 3);

        if (type == 0)
        {
            enemyType = EnemyInfo.types.Air;
            spriteRenderer.color = Color.white;
        }
        else if (type == 1)
        {
            enemyType = EnemyInfo.types.Water;
            spriteRenderer.color = Color.blue;
        }
        else if (type == 2)
        {
            enemyType = EnemyInfo.types.Fire;
            spriteRenderer.color = Color.red;
        }
        else if (type == 3)
        {
            enemyType = EnemyInfo.types.Nature;
            spriteRenderer.color = Color.green;
        }
    }

    private void Update()
    {
        if(timer > TimeToSpawn)
        {
            SpawnEnemy(enemyType);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        startScale -= Time.fixedDeltaTime / 2;
        transform.localScale = new Vector3(startScale, startScale, startScale);
    }

    private void SpawnEnemy(EnemyInfo.types type)
    {
        int hard = Random.Range(1, 5);
        int level = 1;

        if (hard == 1) level = Random.Range(1, waves.currentWaveEnemy);

        if (type == EnemyInfo.types.Air)
        {
            Instantiate(Airtypes[level - 1], transform.position, Quaternion.identity);
        }
        else if (type == EnemyInfo.types.Water)
        {
            Instantiate(Watertypes[level - 1], transform.position, Quaternion.identity);
        }
        else if (type == EnemyInfo.types.Fire)
        {
            Instantiate(Firetypes[level - 1], transform.position, Quaternion.identity);
        }
        else if (type == EnemyInfo.types.Nature)
        {
            Instantiate(Naturetypes[level - 1], transform.position, Quaternion.identity);
        }
    }
}
