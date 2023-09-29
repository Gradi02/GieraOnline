using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgPopup : MonoBehaviour
{
    private TextMeshPro text;
    private float startFontSize = 20f;
    private float speed = 8;
    private Vector2 velocity;
    private float offsetSpeed = 5;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.fontSize = startFontSize;
    }

    private void Update()
    {
        text.fontSize -= speed * Time.deltaTime;
        speed += 20 * Time.deltaTime;    
        transform.Translate(velocity * offsetSpeed * Time.deltaTime);

        if(text.fontSize <= 0) Destroy(gameObject);
    }

    public void SetVelocity(Vector2 vel)
    {
        velocity = vel;
    }
}