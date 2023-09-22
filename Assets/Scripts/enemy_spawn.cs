using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawn : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    private Vector3 random_cord;

    private void Update()
    {
        random_cord = new Vector3(player.transform.position.x, player.transform.position.y+10, player.transform.position.z);
    }
    public void Spawner()
    {
        Instantiate(enemy, random_cord, Quaternion.identity);

    }
}
