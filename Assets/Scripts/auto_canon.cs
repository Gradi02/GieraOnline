using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.Rendering;

public class Auto_canon : MonoBehaviour
{
    public string enemyTag = "Enemy"; // Tag obiektów wroga
    private List<Transform> activeEnemies = new List<Transform>(); // Lista aktywnych wrogów

    public GameObject auto_canon;
    private float cooldown = 0;
    private int bulletSpeed = 12;
    Vector3 dir;
    void Update()
    {
        UpdateActiveEnemiesList();

        if (activeEnemies.Count > 0)
        {
            Transform closestEnemy = FindClosestEnemy();
            if (closestEnemy != null)
            {
                float distance = Vector3.Distance(transform.position, closestEnemy.position);
                Debug.Log("Odleg³oœæ do najbli¿szego wroga: " + distance);


                if(distance <= 15 && cooldown <= Time.time)
                {
                    dir = (closestEnemy.position - transform.position).normalized;
                    Instantiate(auto_canon, transform.position, Quaternion.identity);
                    cooldown = Time.time+2;
                }
            }
        }
        auto_canon.transform.Translate(dir * bulletSpeed * Time.deltaTime);
    }


    void UpdateActiveEnemiesList()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag(enemyTag);

        // Aktualizuj listê aktywnych wrogów
        activeEnemies.Clear();
        foreach (GameObject enemyObject in enemyObjects)
        {
            activeEnemies.Add(enemyObject.transform);
        }
    }

    Transform FindClosestEnemy()
    {
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform enemy in activeEnemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
