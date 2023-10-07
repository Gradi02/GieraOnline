using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chain : MonoBehaviour
{
    public int damage = 5;
    private float cooldown = 5;
    private List<Transform> activeEnemies = new List<Transform>(); // Lista aktywnych wrogów
    float distance = 0;
    public GameObject chain_bullet;
    private float chain_cooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < 5; i++)
        {
            chain_cooldown = Time.time + 0.2f;
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("QWIEQIEEQUOIWUE");
                collision.gameObject.GetComponent<EnemyInfo>().chain_hit = true;

                if (distance < 25 && activeEnemies.Count > 0 && chain_cooldown <= Time.time)
                {
                    Debug.Log("asdsadasdsadasdsada");
                    chain_bullet.transform.position = FindClosestEnemy().transform.position;
                }
            }
        }
        Destroy(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateActiveEnemiesList();         
            if (activeEnemies.Count > 0)
            {
                Transform closestEnemy = FindClosestEnemy();
                if (closestEnemy != null)
                {
                    distance = Vector3.Distance(transform.position, closestEnemy.position);
                    Debug.Log("Odleg³oœæ do najbli¿szego wroga: " + distance);
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
