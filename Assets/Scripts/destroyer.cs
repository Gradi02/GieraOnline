using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 2);
    }
}
