using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Components
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    #endregion

    #region Serialized Fields
    [SerializeField] private Transform handsAndWeapon;
    [SerializeField] private Camera cam;
    [SerializeField] private float moveSpeed;
    #endregion

    #region Private Fields
    private Vector2 movement;
    private Vector2 mousePos;
    private bool isFacingRight;
    private float mouseX;
    private float dir;
    private bool isDirNegative;
    #endregion

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
;
        
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        mouseX = mousePos.x;
        dir = mouseX - rb.position.x;
       
        if (isFacingRight && dir < 0)
        {
            rb.transform.Rotate(0, 180, 0);
            isFacingRight = false;
        }
        else if(!isFacingRight && dir > 0)
        {
            rb.transform.Rotate(0, -180, 0);
            isFacingRight = true;
        }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        handsAndWeapon.rotation = Quaternion.Euler(0f, 0f, angle);
    
    }
}
