using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class waves : MonoBehaviour
{
    public static int wave = 0;
    public static int currentWaveEnemy = 3;
    public static int currentEnemyLevel = 1;
    public static float enemyHpMultiplier = 1;
    public static int mutedEnemyChance = 20;

    public static bool spawning = false;

    private GameObject player;
    private float nextSpawn = 1;
    private float spawnTime = 4;
    private float timer = 0;
    readonly private string wavewinText = "Wave Completed!!!";
    private bool win = false;
    private float nextlett = 0;
    private int i = 0;

    [SerializeField] private GameObject spawner;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject PlayUI;
    [SerializeField] private TextMeshProUGUI wavecomplete;

    private bool end = false;
    private Color bg = Color.black;
    private Color loseTitle = Color.white;
    private Color loseTitle2 = Color.white;
    private Color buttons = Color.white;
    [SerializeField] private GameObject loseBg;
    [SerializeField] private GameObject loseUI1, loseUI2;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject b1, b2;
    private float duration = 0;
    private int rain_max = 0;

    public Light2D light2D;
    public Light2D light2DFlash;
    public ParticleSystem particleSystem_rain;
    public GameObject stormtitle;
    private bool storm = false;
    private float flashTimer = 0;

    public GameObject[] zoom_out;
    public GameObject[] overlay_zoom;
    public AnimationClip zoomIn, zoomOut;
    private void Start()
    {
        particleSystem_rain.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        spawning = false;
        timerText.text = "Next: 20 s";
        wavecomplete.gameObject.SetActive(false);

        loseTitle = scoreText.color;
        loseTitle2 = scoreText.color;
        bg.a = 0;
        loseTitle.a = 0;
        loseTitle2.a = 0;
        buttons.a = 0;

        wave = 0;
    }

    [ContextMenu("start")]
    public void WaveStart()
    {
        FindObjectOfType<AudioManager>().Play("wave start");
        FindObjectOfType<AudioManager>().Play("click");
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

        FindObjectOfType<AudioManager>().SetPriority("music", 0.3f);
        Rain();
        player.transform.GetChild(1).Find("rod of discord").transform.GetComponent<MouseDistanceTracker>().SetUsage();

        foreach(GameObject g in overlay_zoom)
        {
            g.GetComponent<Animation>().clip = zoomIn;
            g.GetComponent<Animation>().Play();
        }
    }

    [ContextMenu("timesup")]
    public void TimesUp()
    {
        FindObjectOfType<AudioManager>().Play("wave end");
        spawning = false;
        upgrades_text.money_upgrade += player.GetComponent<PlayerInfo>().enemyKilledPerRound;
        wave++;
        SetTime();
        timerText.text = "Next: " + timer.ToString() + " s";
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
        FindObjectOfType<AudioManager>().SetPriority("music", 0.1f);
        FindObjectOfType<AudioManager>().Stop("rain");

        StartCoroutine(Rain_OFF());
        light2D.intensity = 1f;
        particleSystem_rain.gameObject.SetActive(false);
        storm = false;
    }

    public void HideBuildingUI()
    {
        foreach(GameObject g in zoom_out)
        {
            g.GetComponent<Animation>().clip = zoomOut;
            g.GetComponent<Animation>().Play();
        }

        foreach(GameObject g in overlay_zoom)
        {
            g.transform.localScale = Vector3.zero;
        }
    }

    private void Rain()
    {
        int temp = Random.Range(0, 100);
        Debug.Log("temp: " + temp + "   rainMax: " + rain_max);
        if (temp < rain_max) //temp < rain_max
        {
            StartCoroutine(Rain_ON());
            FindObjectOfType<AudioManager>().Play("rain");
            particleSystem_rain.gameObject.SetActive(true);
            rain_max = 0;
            storm = true;
            stormtitle.GetComponent<Animation>().Play();
        }
        else
        {
            light2D.intensity = 1f;
            particleSystem_rain.gameObject.SetActive(false);
            rain_max += 5;
        }
    }

    IEnumerator Rain_ON()
    {
        while (light2D.shapeLightFalloffSize >= 40f)
        {
            yield return new WaitForSeconds(0.01f);
            light2D.shapeLightFalloffSize -= 1;
        }
    }

    private void RandomFlash()
    {
        int randX = Random.Range(-20, 20);
        int randY = Random.Range(-20, 20);

        Vector3 pos = player.transform.position;
        pos += new Vector3(randX, randY);

        FindObjectOfType<AudioManager>().Play("thunder");

        Light2D flash = Instantiate(light2DFlash, pos, Quaternion.identity);
        StartCoroutine(Flash(flash));
    }

    IEnumerator Rain_OFF()
    {
        while (light2D.shapeLightFalloffSize <= 250f)
        {
            yield return new WaitForSeconds(0.01f);
            light2D.shapeLightFalloffSize += 1;
        }
    }

    IEnumerator Flash(Light2D flash)
    {
        while(flash.shapeLightFalloffSize <= 250)
        {
            yield return new WaitForSeconds(0.005f);
            flash.shapeLightFalloffSize += 20;
            flash.intensity += 0.2f;
        }
        if (flash.shapeLightFalloffSize >= 250) StartCoroutine(FlashOff(flash));
    }

    IEnumerator FlashOff(Light2D flash)
    {
        while (flash.shapeLightFalloffSize >= 0)
        {
            yield return new WaitForSeconds(0.005f);
            flash.shapeLightFalloffSize -= 20;
            flash.intensity -= 0.2f;

            if (flash.intensity <= 0) flash.intensity = 0;
        }
    }
    private void Update()
    {
        if (spawning)
        {
            if(Time.time >= nextSpawn)
            {
                nextSpawn = Time.time + spawnTime;
                Vector2 spawnPos = RandomCord(20);

                for(int i = 0; i < currentWaveEnemy; i++)
                {
                    Vector2 spawnOffset = RandomCord(7);
                    Instantiate(spawner, spawnPos + spawnOffset, Quaternion.identity);
                }
            }
        }

        if(end)
        {
            if (wave == 1 || wave == 2)
            {
                if(player.GetComponent<PlayerInfo>().enemyKilled <= 1)
                    scoreText.text = "You Survived " + (wave - 1) + " wave!\nYou killed " + player.GetComponent<PlayerInfo>().enemyKilled + " enemy!";
                else
                    scoreText.text = "You Survived " + (wave - 1) + " wave!\nYou killed " + player.GetComponent<PlayerInfo>().enemyKilled + " enemies!";
            }
            else if (wave >= 3)
            {
                if (player.GetComponent<PlayerInfo>().enemyKilled <= 1)
                    scoreText.text = "You Survived " + (wave - 1) + " waves!\nYou killed " + player.GetComponent<PlayerInfo>().enemyKilled + " enemy!";
                else
                    scoreText.text = "You Survived " + (wave - 1) + " waves!\nYou killed " + player.GetComponent<PlayerInfo>().enemyKilled + " enemies!";
            }

            player.GetComponent<PlayerInfo>().currentHp = 0;
            loseBg.SetActive(true);
            duration += Time.deltaTime;
            float alphaValue1 = Mathf.Lerp(0, 1, 0.1f * duration * 10);
            float alphaValue2 = Mathf.Lerp(0, 0.5f, 0.1f * duration * 10);

            bg.a = alphaValue1;
            loseTitle.a = alphaValue2;
            loseTitle2.a = alphaValue1;
            buttons.a = alphaValue1;

            loseBg.GetComponent<UnityEngine.UI.Image>().color = loseTitle;
            loseUI1.GetComponent<UnityEngine.UI.Image>().color = bg;
            loseUI2.GetComponent<TextMeshProUGUI>().color = loseTitle2;
            scoreText.color = loseTitle2;
            b1.GetComponent<UnityEngine.UI.Image>().color = buttons;
            b2.GetComponent<UnityEngine.UI.Image>().color = buttons;
            b1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = loseTitle2;
            b2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = loseTitle2;
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
        else
        {
            player.transform.position = Vector3.Lerp(player.transform.position, Vector3.zero, 0.01f);
        }

        if (win)
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
                foreach (GameObject g in overlay_zoom)
                {
                    g.GetComponent<Animation>().clip = zoomOut;
                    g.GetComponent<Animation>().Play();
                }
                GetComponent<Artefacts>().SpawnImg();
            }
        }

        if(spawning && storm)
        {
            flashTimer += Time.fixedDeltaTime;
        }

        if(flashTimer > 1)
        {
            flashTimer = 0;
            int thunder = Random.Range(0, 8);
            if (thunder < 2) RandomFlash();
        }
    }

    private void SetTime()
    {
        if (wave>0 && wave<2)             { timer = 20; currentEnemyLevel = 1; currentWaveEnemy = 4;  spawnTime = 5;    mutedEnemyChance = 20; }
        else if (wave>=2 && wave<4)       { timer = 25; currentEnemyLevel = 1; currentWaveEnemy = 5;  spawnTime = 4f;   mutedEnemyChance = 19; }
        else if (wave >= 4 && wave < 6)   { timer = 30; currentEnemyLevel = 2; currentWaveEnemy = 6;  spawnTime = 4f;   mutedEnemyChance = 18; }
        else if (wave >= 6 && wave < 9)   { timer = 35; currentEnemyLevel = 2; currentWaveEnemy = 6;  spawnTime = 3.5f; mutedEnemyChance = 17; }
        else if (wave >= 9 && wave < 12)  { timer = 40; currentEnemyLevel = 3; currentWaveEnemy = 7;  spawnTime = 3.5f; mutedEnemyChance = 16; }
        else if (wave >= 12 && wave < 15) { timer = 45; currentEnemyLevel = 3; currentWaveEnemy = 8;  spawnTime = 3;    mutedEnemyChance = 15; }
        else if (wave >= 15 && wave < 20) { timer = 50; currentEnemyLevel = 4; currentWaveEnemy = 9;  spawnTime = 2.5f; mutedEnemyChance = 14; }
        else if (wave >= 20 && wave < 25) { timer = 60; currentEnemyLevel = 4; currentWaveEnemy = 9;  spawnTime = 2.5f; mutedEnemyChance = 13; }
        else if (wave >= 25 && wave < 30) { timer = 70; currentEnemyLevel = 4; currentWaveEnemy = 9;  spawnTime = 2.5f; mutedEnemyChance = 12; }
        else                              
        {
            timer = 80; 
            currentEnemyLevel = 5; 
            spawnTime = 2;
            mutedEnemyChance = 10;

            currentWaveEnemy = 10 + WaveCalculate(); 
        }
    }

    private int WaveCalculate()
    {
        int add = wave;
        add -= 30;
        add /= 10;
        Mathf.CeilToInt(add);
        return add;
    }

    private Vector2 RandomCord(int max) 
    {
        int x = Random.Range(-max, max);
        int y = Random.Range(-max, max);
        
        
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

    public void EndGame()
    {
        spawning = false;
        timer = 0;
        timerText.color = Color.white;
        player.GetComponent<PlayerInfo>().poisonTime = 0;

        foreach (GameObject spawner in GameObject.FindGameObjectsWithTag("Spawner"))
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

        FindObjectOfType<AudioManager>().SetPriority("music", 0.1f);
        FindObjectOfType<AudioManager>().Stop("rain");

        StartCoroutine(Rain_OFF());
        light2D.intensity = 1f;
        particleSystem_rain.gameObject.SetActive(false);
        storm = false;

        end = true;
    }
}
