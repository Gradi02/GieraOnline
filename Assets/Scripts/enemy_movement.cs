using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    private GameObject player;
    private Vector3 pos;
    public float speed = 1f;
    // Update is called once per frame

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        pos = player.transform.position;
        transform.Translate(speed * Time.deltaTime * pos);
    }
}
