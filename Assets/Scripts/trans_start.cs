using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class trans_start : MonoBehaviour
{
    private float duration = 0;
    public RawImage trans;
    private bool transition = true;

    public float cooldown = 0;

    private void Start()
    {
        cooldown += Time.time + 1f;
    }
    void Update()
    {
        if (cooldown <= Time.time) transition = true;


        if (transition)
        {
            duration += Time.deltaTime;
            RawImage rawImage = trans.GetComponent<RawImage>();
            Color currentColor = rawImage.color;
            float alphaValue = Mathf.Lerp(1, 0, 0.1f * duration * 3);
            currentColor.a = alphaValue;
            rawImage.color = currentColor;
            if (alphaValue == 0) trans.gameObject.SetActive(false);
        }
        
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        ResetStats();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResetStats();
    }

    private void ResetStats()
    {
        waves.wave = 0;
        waves.currentWaveEnemy = 3;
        waves.currentEnemyLevel = 1;
        waves.enemyHpMultiplier = 1;
        waves.spawning = false;
    }
}
