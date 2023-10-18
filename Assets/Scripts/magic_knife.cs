using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magic_knife : MonoBehaviour
{
    public GameObject knife;
    public GameObject anchor;
    private int level;
    public int angle;
    private int damage = 5;

    public Transform knife1;
    public Transform knife2;
    public Transform knife3;
    public Transform knife4;

    public knife knife_script;
    private int lastlevel;
    private void Start()
    {
        level = GetComponent<ArtefactManager>().GetLevel();
        lastlevel = level;
        angle = angle_calc();

        knife1 = transform.GetChild(1);
        knife2 = transform.GetChild(2);
        knife3 = transform.GetChild(3);
        knife4 = transform.GetChild(4);
    }
    private void Update()
    {
        level = GetComponent<ArtefactManager>().GetLevel();

        if(level != lastlevel)
        {
            lastlevel = level;
            angle = angle_calc();
        }

        damage = 5 + level;

        if (level == 1)
        {
            knife1.gameObject.SetActive(true);
            knife2.gameObject.SetActive(false);
            knife3.gameObject.SetActive(false);
            knife4.gameObject.SetActive(false);

            knife1.GetComponent<SpriteRenderer>().color = Color.white;
            knife2.GetComponent<SpriteRenderer>().color = Color.white;
            knife3.GetComponent<SpriteRenderer>().color = Color.white;
            knife4.GetComponent<SpriteRenderer>().color = Color.white;
        }

        else if (level == 2)
        {
            knife1.gameObject.SetActive(true);
            knife2.gameObject.SetActive(true);
            knife3.gameObject.SetActive(false);
            knife4.gameObject.SetActive(false);

            knife1.GetComponent<SpriteRenderer>().color = Color.white;
            knife2.GetComponent<SpriteRenderer>().color = Color.white;
            knife3.GetComponent<SpriteRenderer>().color = Color.white;
            knife4.GetComponent<SpriteRenderer>().color = Color.white;
        }

        else if (level == 3)
        {
            knife1.gameObject.SetActive(true);
            knife2.gameObject.SetActive(true);
            knife3.gameObject.SetActive(true);
            knife4.gameObject.SetActive(false);

            knife1.GetComponent<SpriteRenderer>().color = Color.white;
            knife2.GetComponent<SpriteRenderer>().color = Color.white;
            knife3.GetComponent<SpriteRenderer>().color = Color.white;
            knife4.GetComponent<SpriteRenderer>().color = Color.white;
        }

        else if (level == 4)
        {
            knife1.gameObject.SetActive(true);
            knife2.gameObject.SetActive(true);
            knife3.gameObject.SetActive(true);
            knife4.gameObject.SetActive(true);

            knife1.GetComponent<SpriteRenderer>().color = Color.white;
            knife2.GetComponent<SpriteRenderer>().color = Color.white;
            knife3.GetComponent<SpriteRenderer>().color = Color.white;
            knife4.GetComponent<SpriteRenderer>().color = Color.white;
        }

        else if (level == 5)
        {
            knife1.gameObject.SetActive(true);
            knife2.gameObject.SetActive(true);
            knife3.gameObject.SetActive(true);
            knife4.gameObject.SetActive(true);

            knife1.GetComponent<SpriteRenderer>().color = Color.yellow;
            knife2.GetComponent<SpriteRenderer>().color = Color.yellow;
            knife3.GetComponent<SpriteRenderer>().color = Color.yellow;
            knife4.GetComponent<SpriteRenderer>().color = Color.yellow;

            damage += 5;
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    public int angle_calc()
    {
        if (level == 0) return 0;
        angle = 360 / level;

        if (angle == 360)
        {
            knife1.GetComponent<knife>().currentAngle = 0;
        }

        if (angle == 180)
        {
            knife1.GetComponent<knife>().currentAngle = 0;
            knife2.GetComponent<knife>().currentAngle = 180;
        }

        if (angle == 90)
        {
            knife1.GetComponent<knife>().currentAngle = 0;
            knife2.GetComponent<knife>().currentAngle = 90;
            knife3.GetComponent<knife>().currentAngle = 180;
            knife4.GetComponent<knife>().currentAngle = 270;
        }

        if (angle == 120)
        {
            knife1.GetComponent<knife>().currentAngle = 0;
            knife2.GetComponent<knife>().currentAngle = 120;
            knife3.GetComponent<knife>().currentAngle = 240;
        }

        return angle;
    }
}