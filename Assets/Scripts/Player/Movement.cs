using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;

    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 newPos = new Vector2(speed * horizontal * Time.fixedDeltaTime, speed * vertical * Time.fixedDeltaTime);
        rb.MovePosition(rb.transform.position + newPos);
    }
}
