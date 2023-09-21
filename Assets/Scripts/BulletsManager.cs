using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    private float bulletSpeed = 6;
    void Start()
    {
        Destroy(this.gameObject, 10);
    }

    void Update()
    {
        transform.position += bulletSpeed * Time.deltaTime * transform.up;
    }
}
