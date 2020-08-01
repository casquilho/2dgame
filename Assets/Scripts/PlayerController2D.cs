using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    public bool isGrounded;
    public bool isGroundedL;
    public bool isGroundedR;

    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    Transform groundCheckL;
    [SerializeField]
    Transform groundCheckR;

    public float runSpeed = 1.5f;
    public float jumpSpeed = 3;
    
    public float ySpeed;
  
    public bool doubleJump;
    public bool canDoubleJump;
    public bool hasJetPack;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) 
             isGrounded = true;
        else isGrounded = false;

        if (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")))
            isGroundedL = true;
        else isGroundedL = false;

        if (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
            isGroundedR = true;
        else isGroundedR = false;

        ySpeed = rb.velocity.y;
        if (hasJetPack)
        {
            JetPackMovement();
        }
        else {
            //IS ON THE GROUND
            if (isGrounded || isGroundedL || isGroundedR)
            {
                if (canDoubleJump)
                    doubleJump = true;

                if (Input.GetKey("d") || Input.GetKey("right"))
                {
                    if (Input.GetKeyDown("space"))
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                        animator.Play("jump_animation");
                    }
                    else
                    {
                        rb.velocity = new Vector2(runSpeed, rb.velocity.y);
                        animator.Play("animation_run");
                        spriteRenderer.flipX = false;
                    }
                }
                else
                if (Input.GetKey("a") || Input.GetKey("left"))
                {
                    if (Input.GetKeyDown("space"))
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                        animator.Play("jump_animation");
                    }
                    else {
                        rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
                        animator.Play("animation_run");
                        spriteRenderer.flipX = true;
                    }
                }
                else
                if (Input.GetKeyDown("space"))
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    animator.Play("jump_animation");
                }
                else
                {
                    animator.Play("animation_idle");
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }

            }
            //IS ON THE AIR
            else
            {
                if (rb.velocity.y < 0)
                    animator.Play("fall_animation");
                else
                    animator.Play("jump_animation");


                if (Input.GetKey("d") || Input.GetKey("right"))
                {
                    if (Input.GetKeyDown("space") && (rb.velocity.y < jumpSpeed / 2) && doubleJump)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * .7f);
                        animator.Play("jump_animation");
                        doubleJump = false;
                    }
                    else
                        rb.velocity = new Vector2(runSpeed, rb.velocity.y);
                    spriteRenderer.flipX = false;
                }
                else
                if (Input.GetKey("a") || Input.GetKey("left"))
                {
                    if (Input.GetKey("space") && (rb.velocity.y < jumpSpeed / 2) && doubleJump)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * .7f);
                        animator.Play("jump_animation");
                        doubleJump = false;
                    }
                    else
                        rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
                    spriteRenderer.flipX = true;
                }
                else
                if (Input.GetKey("space") && (rb.velocity.y < jumpSpeed / 2) && doubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    animator.Play("jump_animation");
                    doubleJump = false;
                }
                else
                    rb.velocity = new Vector2(0, rb.velocity.y);

            }
        }
        


        
        /*if (Input.GetKey("d") || Input.GetKey("right")){
            rb.velocity = new Vector2(runSpeed, rb.velocity.y);
            if (isGrounded || isGroundedL || isGroundedR)
                animator.Play("animation_run");
            else
                animator.Play("fall_animation");
            spriteRenderer.flipX = false;
        }
        else 
        if (Input.GetKey("a") || Input.GetKey("left")){
            rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
            if (isGrounded || isGroundedL || isGroundedR)
                animator.Play("animation_run");
            else
                animator.Play("fall_animation");
            spriteRenderer.flipX = true;
        }
        else{
            if (isGrounded || isGroundedL || isGroundedR)
                animator.Play("animation_idle");
            rb.velocity = new Vector2(0, rb.velocity.y);
        } 

        if (Input.GetKey("space") && (isGrounded || isGroundedL || isGroundedR)){ 
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            animator.Play("jump_animation");
        }*/
    }
    private void JetPackMovement()
    {

    }
}
