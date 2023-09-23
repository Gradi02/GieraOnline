using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[] rotationSprites = new Sprite[8];

    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        SetRotationSprite();
    }

    private void FixedUpdate()
    {
        Vector3 newPos = new Vector2(speed * horizontal * Time.fixedDeltaTime, speed * vertical * Time.fixedDeltaTime);
        rb.MovePosition(rb.transform.position + newPos);
    }

    private void SetRotationSprite()
    {
        /*
        [0] góra
        [1] góra prawo
        [2] prawo
        [3] dó³ prawo
        [4] dó³
        [5] dó³ lewo
        [6] lewo
        [7] góra lewo
        */

        if (horizontal == 0 && vertical > 0) spriteRenderer.sprite = rotationSprites[0];
        else if (horizontal > 0 && vertical > 0) spriteRenderer.sprite = rotationSprites[1];
        else if (horizontal > 0 && vertical == 0)
        {
            spriteRenderer.sprite = rotationSprites[2];
            spriteRenderer.flipX = false;
        }
        else if (horizontal > 0 && vertical < 0) spriteRenderer.sprite = rotationSprites[3];
        else if (horizontal == 0 && vertical < 0) spriteRenderer.sprite = rotationSprites[4];
        else if (horizontal < 0 && vertical < 0) spriteRenderer.sprite = rotationSprites[5];
        else if (horizontal < 0 && vertical == 0)
        {
            spriteRenderer.sprite = rotationSprites[6]; 
            spriteRenderer.flipX = true;
        }
        else if (horizontal < 0 && vertical > 0) spriteRenderer.sprite = rotationSprites[7];
    }
}
