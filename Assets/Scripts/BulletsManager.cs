using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    public float bulletSpeed = 10;
    public ParticleSystem bulletparticle;
    void Start()
    {
        Destroy(this.gameObject, 10);
    }

    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * transform.right;
    }
}
