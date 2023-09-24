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

    [SerializeField] private Transform spawner;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = false;
        spawner.transform.localPosition = new Vector3(1.1f, 2, 0);
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
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
            spawner.transform.localPosition = new Vector3(1.1f, 2, 0);
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
            spawner.transform.localPosition = new Vector3(-1.1f, 2, 0);
        }
    }
}
