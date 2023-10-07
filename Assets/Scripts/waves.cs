using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class waves : MonoBehaviour
{
    public static int wave = 0;
    public static int currentWaveEnemy = 3;
    public static int currentEnemyLevel = 1;
    public static float enemyHpMultiplier = 1;

    public static bool spawning = false;

    private GameObject player;
    private float nextSpawn = 1;
    private float spawnTime = 4;
    private float timer = 0;
    readonly private string wavewinText = "Wave_Completed!!!";
    private bool win = false;
    private float nextlett = 0;
    private int i = 0;

    [SerializeField] private GameObject spawner;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject PlayUI;
    [SerializeField] private TextMeshProUGUI wavecomplete;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawning = false;
        timerText.text = "Next: 20s";
        upgradeUI.SetActive(true);
        PlayUI.SetActive(false);
        wavecomplete.gameObject.SetActive(false);
    }

    [ContextMenu("start")]
    public void WaveStart()
    {
        wave++;
        waveText.text = "Wave " + wave;
        SetTime();
        timerText.text = timer.ToString();
        
        if(wave>10 && wave<=200) enemyHpMultiplier = (wave/10);
        else if(wave>200) enemyHpMultiplier = 20;
        spawning = true;

        player.GetComponent<PlayerInfo>().enemyKilledPerRound = 0;
        player.GetComponent<PlayerInfo>().SetStats();

        upgradeUI.SetActive(false);
        PlayUI.SetActive(true);
    }

    [ContextMenu("timesup")]
    public void TimesUp()
    {
        spawning = false;
        upgrades_text.money_upgrade += player.GetComponent<PlayerInfo>().enemyKilledPerRound;
        wave++;
        SetTime();
        timerText.text = "Next: " + timer.ToString() + "s";
        timer = 0;
        wave--;
        timerText.color = Color.white;
        player.GetComponent<PlayerInfo>().poisonTime = 0;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<EnemyInfo>().DestroyEnemy();
        }

        foreach(GameObject spawner in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            Destroy(spawner);
        }

        foreach (GameObject b in GameObject.FindGameObjectsWithTag("bullet"))
        {
            Destroy(b);
        }

        foreach (GameObject b in GameObject.FindGameObjectsWithTag("poison"))
        {
            Destroy(b);
        }

        WinAnimation();
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
        else
        {
            player.transform.position = Vector3.Lerp(player.transform.position, Vector3.zero, 0.001f);
        }
    }

    private void FixedUpdate()
    {
        if (spawning)
        {
            timer -= Time.fixedDeltaTime;
            timerText.text = Mathf.FloorToInt(timer).ToString();

            if (timer <= 6) timerText.color = Color.red;

            if (timer <= 0) TimesUp();
        }

        if(win)
        {   
            if (Time.time >= nextlett)
            {
                wavecomplete.text += wavewinText[i];
                i++;
                nextlett = Time.time + 0.15f;
            }

            if (wavecomplete.text.Length > 15)
            {
                win = false;
                wavecomplete.text = string.Empty;
                wavecomplete.gameObject.SetActive(false);
                nextlett = 0;
                i = 0;
                PlayUI.SetActive(false);
                //upgradeUI.SetActive(true);
                GetComponent<Artefacts>().SpawnImg();
            }
        }
    }

    private void SetTime()
    {
        if (wave == 1)                    { timer = 20; currentEnemyLevel = 1; currentWaveEnemy = 5;  spawnTime = 4;    }
        else if (wave == 2)               { timer = 25; currentEnemyLevel = 1; currentWaveEnemy = 5;  spawnTime = 3.5f; }
        else if (wave == 3 || wave == 4)  { timer = 30; currentEnemyLevel = 2; currentWaveEnemy = 6;  spawnTime = 3.5f; }
        else if (wave == 5 || wave == 6)  { timer = 40; currentEnemyLevel = 2; currentWaveEnemy = 6;  spawnTime = 3;    }
        else if (wave == 7 || wave == 8)  { timer = 50; currentEnemyLevel = 2; currentWaveEnemy = 7;  spawnTime = 3;    }
        else if (wave > 8 && wave <= 20)  { timer = 60; currentEnemyLevel = 3; currentWaveEnemy = 8;  spawnTime = 3;    }
        else if (wave > 20 && wave <= 40) { timer = 70; currentEnemyLevel = 4; currentWaveEnemy = 9;  spawnTime = 2.5f; }
        else                              { timer = 80; currentEnemyLevel = 5; currentWaveEnemy = 10; spawnTime = 2.5f; }
    }

    private Vector2 RandomCord() 
    {
        int x = Random.Range(-30, 30);
        int y = Random.Range(-30, 30);
        
        
        return (new Vector3(x,y));
    }

    private void WinAnimation()
    {
        wavecomplete.gameObject.SetActive(true);
        wavecomplete.text = string.Empty;
        win = true;
    }

    public void SetUpUI()
    {
        upgradeUI.SetActive(true);
    }
}
