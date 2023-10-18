using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MouseDistanceTracker : MonoBehaviour
{
    public GameObject player;
    private bool can_teleport = true;
    private bool isTeleporting = false;

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
            can_teleport = true;
        }

        float distance = Vector3.Distance(playerPosition, worldMousePosition);

        Debug.Log("Odleg³oœæ: " + distance);

        if (Input.GetKeyDown(KeyCode.Mouse1) && distance <= 30f && !isTeleporting && can_teleport)
        {
            StartCoroutine(TeleportAfterDelay(worldMousePosition, 1.0f)); // Oczekaj 1 sekundê przed teleportacj¹
        }
    }

    IEnumerator TeleportAfterDelay(Vector3 targetPosition, float delay)
    {
        isTeleporting = true;
        player.GetComponent<Movement>().enabled = false;

        yield return new WaitForSeconds(delay);

        player.transform.position = new Vector3(targetPosition.x, targetPosition.y, player.transform.position.z);
        player.GetComponent<Movement>().enabled = true;
        isTeleporting = false;
    }
}