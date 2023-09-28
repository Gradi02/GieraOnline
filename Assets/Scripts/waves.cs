using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class waves : MonoBehaviour
{
    public static int wave = 0;
    public static int currentWaveEnemy = 3;
    public static int currentEnemyLevel = 1;

    public static bool spawning = false;

    private GameObject player;
    private float nextSpawn = 1;
    private float spawnTime = 4;
    private float timer = 0;

    [SerializeField] private GameObject spawner;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject upgradeUI;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawning = false;
        timerText.text = "Next: 20s";
        upgradeUI.SetActive(true);
    }

    [ContextMenu("start")]
    public void WaveStart()
    {
        wave++;
        waveText.text = "Wave " + wave;
        SetTime();
        timerText.text = timer.ToString();
        spawning = true;
        upgradeUI.SetActive(false);
    }

    [ContextMenu("timesup")]
    public void TimesUp()
    {
        spawning = false;

        wave++;
        SetTime();
        timerText.text = "Next: " + timer.ToString() + "s";
        timer = 0;
        wave--;
        upgradeUI.SetActive(true);

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyInfo>().DestroyEnemy();
        }

        foreach(GameObject spawner in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            Destroy(spawner);
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
                    Instantiate(spawner, spawnPos, Quaternion.identity);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (spawning)
        {
            timer -= Time.fixedDeltaTime;
            timerText.text = Mathf.FloorToInt(timer).ToString();

            if (timer <= 0) TimesUp();
        }
    }

    private void SetTime()
    {
        if (wave == 1) timer = 20;
        else if (wave == 2) timer = 25;
        else if(wave == 3 || wave == 4) timer = 30;
        else if(wave == 5 || wave == 6) timer = 40;
        else if(wave == 7 || wave == 8) timer = 50;
        else if(wave > 8 && wave <= 20) timer = 60;
        else if(wave > 20 && wave <= 40) timer = 70;
        else timer = 80;
    }

    private Vector2 RandomCord() 
    {
        int x = Random.Range(-30, 30);
        int y = Random.Range(-30, 30);
        
        
        return (new Vector3(x,y));
    }
}
