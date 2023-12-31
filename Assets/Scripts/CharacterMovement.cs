using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    #region Components
    private Rigidbody2D rb;

    private TrailRenderer trailRenderer;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    #endregion
    
    #region Movement
    [Header("Movement")]
    [SerializeField] private Camera cam;
    [SerializeField] private float moveSpeed;

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

    
    #region Dashing
    [Header("Dashing")]
    [SerializeField] private float dashSpeed= 25f;
    [SerializeField] private float dashLength= 1f;
    [SerializeField] private float dashCooldown=1f;
    private bool isDashing;
    private bool canDash;
    #endregion



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        isFacingRight = true;
        canDash = true;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
              StartCoroutine(Dash());
        }
 



    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
       


    } 

    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        isFacingRight = !isFacingRight;

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        handsAndWeapon.rotation = Quaternion.Euler(0f, 0f, angle);
    

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        trailRenderer.emitting = true;
        rb.velocity = movement.normalized * dashSpeed;
        yield return new WaitForSeconds(dashLength);
        isDashing = false;
        trailRenderer.emitting = false;
        
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    
}
