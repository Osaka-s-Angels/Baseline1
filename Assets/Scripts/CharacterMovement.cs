using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float moveSpeed;

    private Vector2 movement;
    private Vector2 mousePos;
    private bool isFacingRight;
    private float mouseX;
    private float dir;
    private bool isDirNegative;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isFacingRight = true;
    }

  
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        mouseX = mousePos.x;
        dir = mouseX - rb.position.x;
       
        if (isFacingRight && dir < 0)
        {
            Turn();
        }
        else if(!isFacingRight && dir > 0)
        {
            Turn();
        }
        
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        
    } 

    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;
    }
}
