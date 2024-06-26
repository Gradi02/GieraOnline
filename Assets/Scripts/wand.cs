using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wand : MonoBehaviour
{
    void Update()
    {
        if (!waves.spawning) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
