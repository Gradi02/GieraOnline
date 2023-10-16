using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseDistanceTracker : MonoBehaviour
{
    public GameObject player;
    private float cooldown;
    private bool can_teleport = true;

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 mousePosition = Input.mousePosition;

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
        RaycastHit2D hit = Physics2D.Raycast(worldMousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("barrier"))
        {
            can_teleport = false;
            Debug.Log("Myszka najecha³a na obiekt z tagiem 'barrier'.");
        }
        else 
        { 
        can_teleport= true;
        }

        float distance = Vector3.Distance(playerPosition, worldMousePosition);

        Debug.Log("Odleg³oœæ: " + distance);

        if (Input.GetKeyDown(KeyCode.Mouse1) && distance <= 20f && cooldown<Time.time && can_teleport)
        {
            player.transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, player.transform.position.z);

            cooldown = Time.time+5f;
        }
    }
}