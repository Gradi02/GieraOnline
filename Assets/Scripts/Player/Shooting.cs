using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shotCooldown;

    private float nextFire = 0;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private ParticleSystem particle;

    void Update()
    {
        //Strzelanie
        if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFire)
        {
            nextFire = Time.time + shotCooldown;
            particle.Play();

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos -= spawnTransform.transform.position;

            float rotationZ = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            GameObject b = Instantiate(bulletPrefab, spawnTransform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
            b.GetComponent<SpriteRenderer>().color = GetComponent<ChangeMode>().GetColor();
        }
    }
}
