using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed;
    [SerializeField] private GameObject player;

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = (mousePos - transform.position).normalized;
        float angelRot = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        angelRot -= 90;

        Quaternion finalRot = Quaternion.Euler(0, 0, angelRot);
        transform.rotation = Quaternion.Slerp(transform.rotation, finalRot, rotationSpeed * Time.deltaTime);

    }
}
