using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class waves : MonoBehaviour
{
    private static int wave = 0;
    private static int currentWaveEnemy = 3;
    private static int currentEnemyTypes = 3;

    private bool spawning = false;

    private GameObject player;
    private float nextSpawn = 0;
    private float spawnTime = 5;

    [SerializeField] private GameObject spawner;
    [SerializeField] private TextMeshProUGUI waveText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawning = false;
    }

    [ContextMenu("start")]
    public void WaveStart()
    {
        wave++;
        waveText.text = "Wave " + wave;
        spawning = true;
    }
    [ContextMenu("timesup")]
    public void TimesUp()
    {
        spawning = false;

        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyInfo>().DestroyEnemy();
        }
    }

    private void Update()
    {
        if(spawning)
        {
            if(Time.time >= nextSpawn)
            {
                nextSpawn = Time.time + spawnTime;

                for(int i = 0; i < currentWaveEnemy; i++)
                {
                    Vector2 spawnPos = RandomCord();
                    int enemyType = Random.Range(0, currentEnemyTypes);
                    GameObject spawnerPos = Instantiate(spawner, spawnPos, Quaternion.identity);
                }
            }
        }
    }

    private Vector2 RandomCord() 
    {
        int x = Random.Range(-30, 30);
        int y = Random.Range(-30, 30);
        
        
        return (new Vector3(x,y));
    }
}
