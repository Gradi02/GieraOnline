using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waves : MonoBehaviour
{
    int wave = 1;
    public GameObject enemy_water;
    public GameObject enemy_air;
    public GameObject enemy_fire;
    public GameObject enemy_nature;
    public GameObject player;
    public Vector3 cords;

    private void Start()
    {
        WaveStart();
    }

    private void RandomCord() {
        int x = Random.Range((int)(player.transform.position.x - 10.0f), (int)(player.transform.position.x + 10));
        int y = Random.Range((int)(player.transform.position.y - 10.0f), (int)(player.transform.position.y + 10));
        cords = new Vector3(x,y);
    }
    private void WaveStart()
    {
        if (wave == 1) StartCoroutine(Wave1());
    }

    IEnumerator Wave1()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
            RandomCord();
            Instantiate(enemy_water, cords, Quaternion.identity);
        }
    }
}
