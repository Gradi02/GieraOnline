using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private float nextFire = 0;
    public bool single_shoot = false;
    public bool double_shoot = false;
    public bool triple_shoot = true;
    public bool spark = false;

    private GameObject b1;
    private GameObject b2;
    private GameObject b3;

    public Material SparkMat;

    public GameObject bulletPrefab;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private ParticleSystem particle;

    private void Start()
    {
        bulletPrefab.GetComponent<SpriteRenderer>().color = Color.white;
    }
    void Update()
    {
        if (!waves.spawning) return;
        //Strzelanie
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFire)
        {
            nextFire = Time.time + GetComponent<PlayerInfo>().GetGunCooldown();
            particle.Play();

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos -= spawnTransform.transform.position;

            float rotationZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            

            //b.GetComponent<SpriteRenderer>().color = GetComponent<ChangeMode>().GetColor();
            if (single_shoot == true)
            {
                b1 = Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
            }

            if (double_shoot == true)
            {
                b2 = Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ + 15));
                b3 = Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ - 15));
            }

            if (triple_shoot == true)
            {
                b1 = Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
                b2 = Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ+25));
                b3 = Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ-25));
            }

            if(spark == true)
            {
                b1.GetComponent<TrailRenderer>().material = SparkMat;
                b2.GetComponent<TrailRenderer>().material = SparkMat;
                b3.GetComponent<TrailRenderer>().material = SparkMat;
            }
        }
    }
}
