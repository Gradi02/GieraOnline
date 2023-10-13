using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private float nextFire = 0;
    public bool single_shoot;
    public bool double_shoot;
    public bool triple_shoot;
    public int shots = 0;
    private bool chain = false;
    private bool book = false;

    public Material sparkyMat;
    public Material normalMat;

    public GameObject bulletPrefab;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private ParticleSystem particle;

    public AudioSource shoot_source;
    public AudioClip shoot1;
    public AudioClip shoot2;
    public AudioClip shoot3;

    private void Start()
    {
        bulletPrefab.GetComponent<SpriteRenderer>().color = Color.white;
        bulletPrefab.GetComponent<TrailRenderer>().material = normalMat;
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
            shots++;

            int x = Random.Range(0, 3);
            if (x == 0) shoot_source.PlayOneShot(shoot1);
            if (x == 1) shoot_source.PlayOneShot(shoot2);
            if (x == 2) shoot_source.PlayOneShot(shoot3);

            //b.GetComponent<SpriteRenderer>().color = GetComponent<ChangeMode>().GetColor();
            if (single_shoot == true)
            {
                Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
            }
            else if (double_shoot == true)
            {
                Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ + 10));
                Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ - 10));
            }
            else if (triple_shoot == true)
            {
                Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
                Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ+20));
                Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ-20));
            }

            single_shoot = true;
            double_shoot = false;
            triple_shoot = false;
        }
    }

    public void SetChain()
    {
        chain = true;
    }

    public bool GetChain()
    {
        return chain;
    }

    public void SetBook()
    {
        book = true;
    }

    public bool GetBook()
    {
        return book;
    }
}
