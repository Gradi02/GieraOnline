using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2move : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position += transform.right * Time.fixedDeltaTime * 12;
    }

    //i tutaj daj kolizje
}
