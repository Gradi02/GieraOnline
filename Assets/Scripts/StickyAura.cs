using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyAura : MonoBehaviour
{
    private Vector3 startPos = new Vector3(1.5f, 1, 0);
    private float timer = 0;
    private GameObject playerFeet;
    private float timetoslow = 6;
    private ParticleSystem slow;
    private float nextslow = 6;

    void Start()
    {
        transform.position = startPos;
        playerFeet = transform.root.gameObject.transform.GetChild(0).gameObject;
        timetoslow = 7 - GetComponent<ArtefactManager>().GetLevel();
        slow = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    void Update()
    {
        timetoslow = 7 - GetComponent<ArtefactManager>().GetLevel();

        if (Time.time >= nextslow)
        {
            slow.Play();
            nextslow = Time.time + timetoslow;
            foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (Vector2.Distance(e.transform.position, playerFeet.transform.position) <= 8)
                {
                    if (!e.GetComponent<EnemyInfo>().isDestroyed())
                        e.GetComponent<EnemyInfo>().SetSlow(GetComponent<ArtefactManager>().GetLevel());
                }
            }
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        transform.localPosition = startPos + new Vector3(0, Mathf.Sin(timer) / 3 , 0);    
    }
}
