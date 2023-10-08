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
    void Update()
    {
        if (cooldown <= Time.time)
        {
            cooldown = Time.time + 2;
            UpdateActiveEnemiesList();

            if (activeEnemies.Count > 0)
            {
                Transform closestEnemy = FindClosestEnemy();
                if (closestEnemy != null)
                {
                    float distance = Vector3.Distance(transform.position, closestEnemy.position);
                    //Debug.Log("Odleg³oœæ do najbli¿szego wroga: " + distance);

                    if (distance <= 15)
                    {
                        Vector3 dir = (closestEnemy.position - transform.position).normalized;
                        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        Instantiate(auto_canon, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
                    }
                }
            }
        }
    }


    private void UpdateActiveEnemiesList()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        // Aktualizuj listê aktywnych wrogów
        activeEnemies.Clear();
        foreach (GameObject enemyObject in enemyObjects)
        {
            activeEnemies.Add(enemyObject.transform);
        }
    }

    private Transform FindClosestEnemy()
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
