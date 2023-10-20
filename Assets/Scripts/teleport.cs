using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseDistanceTracker : MonoBehaviour
{
    public GameObject player;
    private bool isTeleporting = false;
    public TextMeshProUGUI num;
    public Slider cooldown;
    public GameObject teleport;

    private int tpLeft = 0;


    void Update()
    {
        if (!waves.spawning) return;

        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

        if (Input.GetKeyDown(KeyCode.Mouse1) && tpLeft > 0 && cooldown.value == cooldown.minValue && !isTeleporting && worldMousePosition.x < 30 && worldMousePosition.x > -30 && worldMousePosition.y < 30 && worldMousePosition.y > -30)
        {
            StartCoroutine(TeleportAfterDelay(worldMousePosition, .3f)); // Oczekaj 1 sekundê przed teleportacj¹
            tpLeft--;
            num.text = tpLeft.ToString();

            cooldown.value = cooldown.maxValue;
            FindObjectOfType<AudioManager>().Play("teleport");
        }
    }

    private void FixedUpdate()
    {
        if(cooldown.value > cooldown.minValue)
        {
            cooldown.value -= Time.fixedDeltaTime;
        }
    }

    public void SetUsage()
    {
        tpLeft = GetComponent<ArtefactManager>().GetLevel();
        num.text = tpLeft.ToString();
    }

    IEnumerator TeleportAfterDelay(Vector3 targetPosition, float delay)
    {
        isTeleporting = true;
        player.GetComponent<Movement>().enabled = false;
        player.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(delay);

        player.transform.position = new Vector3(targetPosition.x, targetPosition.y, player.transform.position.z);

        player.GetComponent<Movement>().enabled = true;
        isTeleporting = false;
    }
}