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

    [SerializeField] private Transform wand;
    private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = false;
        wand.transform.localPosition = new Vector3(0.6f, -0.5f, 0);
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        speed = GetComponent<PlayerInfo>().GetSpeed();

        SetRotationSprite();
    }

    private void FixedUpdate()
    {
        Vector3 newPos = new Vector2(speed * horizontal * Time.fixedDeltaTime, speed * vertical * Time.fixedDeltaTime);
        rb.MovePosition(rb.transform.position + newPos);
    }

    private void SetRotationSprite()
    {
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
            wand.transform.localPosition = new Vector3(0.6f, -0.5f, 0);
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
            wand.transform.localPosition = new Vector3(-0.6f, -0.5f, 0);
        }
    }
}
