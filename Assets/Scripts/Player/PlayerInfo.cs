using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    private int maxhp = 20;
    public int currentHp = 1;
    private int maxmana = 20;
    public int currentMana = 1;
    private float speed = 8;
    private int damage = 8;
    private float damageMultiplier = 2;
    private int critChance = 5;
    private float critMultiplier = 3;
    private float gunCooldown = 0.40f;

    public int enemyKilledPerRound = 0;
    public int enemyKilled = 0;
    public Slider mana_slider;
    public Slider hp_slider;

    public int poisonTime = 0;
    private float poisonTimer = 0;

    public TextMeshProUGUI hp_txt;
    public TextMeshProUGUI mana_txt;
    public Camera cam;
    public Color normalHp;
    public Color poisonHp;

    public int GetDamage()
    {
        return damage;
    }

    public int GetMaxMana()
    {
        return maxmana;
    }

    public int GetMaxHp()
    {
        return maxhp;
    }

    public void SetMaxMana()
    {
        maxmana += 2;
    }

    public void SetMaxHP()
    {
        maxhp += 2;
    }

    public void SetDamage()
    {
        damage += 1;
    }

    public int GetCritChance()
    {
        return critChance;
    }

    public void SetCritChance()
    {
        critChance += 1;
    }

    public float GetMultiplier()
    {
        return damageMultiplier;
    }

    public void SetMultiplier()
    {
        damageMultiplier += 0.5f;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed()
    {
        speed += 0.25f;
    }

    public float GetCritMulti()
    {
        return critMultiplier;
    }

    public void SetCritMulti()
    {
        critMultiplier += 0.5f;
    }

    public void SetGunCooldown()
    {
        gunCooldown -= 0.02f;
        gunCooldown = Mathf.Round(gunCooldown * 100f) * 0.01f;
    }

    public float GetGunCooldown()
    {
        return gunCooldown;
    }

    public void SetStats()
    {
        hp_slider.maxValue = maxhp;
        mana_slider.maxValue = maxmana;
        currentHp = maxhp;
        currentMana = maxmana;
    }

    public void GetHitted(int dmg_in)
    {
        currentHp -= dmg_in;

        int x = Random.Range(0, 3);
        if (x == 0) FindObjectOfType<AudioManager>().Play("hit1");
        else if (x == 1) FindObjectOfType<AudioManager>().Play("hit2");
        else FindObjectOfType<AudioManager>().Play("hit3");

        CameraShake();
    }

    private void Update()
    {
        /*
        if (currentHp < 8)
        {
            heartbeat_source.PlayOneShot(heartbeat);
            heartbeat_source.loop = true;
        }
        if (currentHp>8) heartbeat_source.loop = false;
        */

        mana_slider.value = currentMana;
        hp_slider.value = currentHp;

        hp_txt.text = "Health: " + currentHp.ToString();
        mana_txt.text = "Mana: " + currentMana.ToString();

        if(poisonTime > 0)
        {
            if(Time.time >= poisonTimer)
            {
                poisonTimer = Time.time + 1;
                poisonTime--;
                currentHp--;
                CameraShake();
            }

            hp_slider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = poisonHp;
        }
        else
        {
            hp_slider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = normalHp;
        }

        if(currentHp <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            foreach(Transform t in gameObject.transform.GetChild(1).transform.GetComponentInChildren<Transform>())
            {
                t.gameObject.SetActive(false);
            }
            GameObject.FindGameObjectWithTag("Manager").GetComponent<waves>().EndGame();
        }
    }

    private void FixedUpdate()
    {
        if(cam.orthographicSize > 15)
        {
            cam.orthographicSize -= Time.fixedDeltaTime;
        }
    }

    public void CameraShake()
    {
        float x = (Random.Range(-.1f, .1f));
        float y = (Random.Range(-.1f, .1f));

        cam.transform.position += new Vector3(x, y, 0f);
        cam.orthographicSize += 0.1f;
    }
}
