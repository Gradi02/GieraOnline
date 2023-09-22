using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour
{
    public bool started = false;

    private float timer = 0;

    [SerializeField] private GameObject title;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !started)
        {
            started = true;
            title.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (title.activeSelf)
        {
            timer += Time.fixedDeltaTime;
            title.transform.localScale = new Vector3(1 + Mathf.Sin(timer)/4, 1 + Mathf.Sin(timer) / 4);
        } 
    }
}
