using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shotCooldown;
    public StartManager startManager;

    private float nextFire = 0;
    private bool startCol = false;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject player;

    void Update()
    {
        //Aktywuj strzelanie
        if (!startManager.started) return;
        else if (!startCol)
        {
            nextFire = Time.time + 1;
            startCol = true;
        }


        //Strzelanie
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFire)
        {
            nextFire = Time.time + shotCooldown;
            Instantiate(bulletPrefab, player.transform.position, player.transform.rotation);
        }
    }
}
