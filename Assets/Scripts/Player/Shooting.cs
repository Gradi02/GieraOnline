using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shotCooldown;

    private float nextFire = 0;
    private bool startCol = false;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject player;

    void Update()
    {


        //Strzelanie
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFire)
        {
            nextFire = Time.time + shotCooldown;
            Instantiate(bulletPrefab, player.transform.position, player.transform.rotation);
        }
    }
}
