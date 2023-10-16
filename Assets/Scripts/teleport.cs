using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MouseDistanceTracker : MonoBehaviour
{
    public GameObject player;
    private float cooldown;

    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 mousePosition = Input.mousePosition;

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

        float distance = Vector3.Distance(playerPosition, worldMousePosition);

        Debug.Log("Odleg³oœæ: " + distance);

        if (Input.GetKeyDown(KeyCode.Mouse1) && distance <= 20f && cooldown<Time.time)
        {
            player.transform.position = new Vector3(worldMousePosition.x, worldMousePosition.y, player.transform.position.z);

            cooldown = Time.time+5f;
        }
    }
}