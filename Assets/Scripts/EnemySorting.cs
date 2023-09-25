using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySorting : MonoBehaviour
{
    void Update()
    {
        foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            int layer = Mathf.RoundToInt(enemy.transform.position.y);

            enemy.GetComponent<SpriteRenderer>().sortingOrder = -layer -(int)enemy.transform.position.y%100;
        }
    }
}
