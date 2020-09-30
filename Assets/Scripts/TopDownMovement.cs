using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;
    GameManager gm;

    bool faceingRight = true;


    private void Start()
    {
        gm = GameManager.Get();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (gm.gameOver) return;

        Vector2 delta = movement * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + delta);

        if (delta.x > 0.01f && !faceingRight)
        {
            faceingRight = true;
            Vector2 scale = transform.localScale;
            transform.localScale = new Vector2(-scale.x, scale.y);
        }
        else if (delta.x < -0.01f && faceingRight)
        {
            faceingRight = false;
            Vector2 scale = transform.localScale;
            transform.localScale = new Vector2(-scale.x, scale.y);
        }
    }
}
