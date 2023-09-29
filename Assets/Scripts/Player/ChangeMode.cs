using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMode : MonoBehaviour
{
    [SerializeField] private RawImage modeImg;
    [SerializeField] private GameObject modeTime;
    private float changeTime = 0;
    private int modeInt = 1;
    void Start()
    {
        modeImg.color = Color.white;
        modeTime.SetActive(false);
    }

    void Update()
    {
        if (Time.time >= changeTime)
        {
            modeTime.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha1) && modeInt != 1)
            {
                SetMode("air");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && modeInt != 2)
            {
                SetMode("water");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && modeInt != 3)
            {
                SetMode("fire");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && modeInt != 4)
            {              
                SetMode("nature");
            }
        }
    }

    private void SetMode(string mode)
    {
        //changeTime = Time.time + GetComponent<PlayerInfo>().GetModeCooldown();
        if (mode == "air")
        {
            modeImg.color = Color.white;
            modeTime.SetActive(false);
            modeInt = 1;
        }
        else if(mode == "water")
        {
            modeImg.color = Color.blue;
            modeTime.SetActive(false);
            modeInt = 2;
        }
        else if (mode == "fire")
        {
            modeImg.color = Color.red;
            modeTime.SetActive(false);
            modeInt = 3;
        }
        else if (mode == "nature")
        {
            modeImg.color = Color.green;
            modeTime.SetActive(false);
            modeInt = 4;
        }
    }

    public Color GetColor()
    {
        if(modeInt == 1) return Color.white;
        else if(modeInt == 2) return Color.blue;
        else if(modeInt == 3) return Color.red;
        else if(modeInt == 4) return Color.green;

        return Color.black;
    }

    public EnemyInfo.types GetMode()
    {
        if (modeInt == 1) return EnemyInfo.types.Air;
        else if (modeInt == 2) return EnemyInfo.types.Water;
        else if (modeInt == 3) return EnemyInfo.types.Fire;
        else if (modeInt == 4) return EnemyInfo.types.Nature;
        
        return EnemyInfo.types.Air;
    }    
}
