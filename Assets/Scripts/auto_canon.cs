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
    private float timer = 0;
    private float timeToShot = 2;
    void Update()
    {
        if (cooldown <= Time.time)
        {
            cooldown = Time.time + timeToShot;
            UpdateActiveEnemiesList();

            if (activeEnemies.Count > 0)
            {
                Transform closestEnemy = FindClosestEnemy();
                if (closestEnemy != null)
                {
                    float distance = Vector3.Distance(transform.position, closestEnemy.position);

                    if (distance <= 15)
                    {
                        Vector3 dir = (closestEnemy.position - transform.position).normalized;
                        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        Instantiate(auto_canon, transform.position, Quaternion.Euler(0.0f, 0.0f, rotationZ));
                    }
                }
            }
        }

        timeToShot = 2 - (GetComponent<ArtefactManager>().GetLevel() / 3);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        transform.localPosition = new Vector2(0, 2.5f + (Mathf.Sin(timer)/2));
    }

    private void UpdateActiveEnemiesList()
    {
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");

        // Aktualizuj listê aktywnych wrogów
        activeEnemies.Clear();
        foreach (GameObject enemyObject in enemyObjects)
        {
            if(!enemyObject.GetComponent<EnemyInfo>().isDestroyed())
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

            if (distanceToEnemy < shortestDistance && !enemy.GetComponent<EnemyInfo>().isDestroyed())
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}
