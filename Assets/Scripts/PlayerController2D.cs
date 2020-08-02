using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    
    public bool isGrounded_L;
    public bool isGrounded_R;
    public bool isGrounded_C;
    public bool isGrounded_CL;
    public bool isGrounded_CR;

    [SerializeField]
    Transform groundCheck_C;
    [SerializeField]
    Transform groundCheck_CL;
    [SerializeField]
    Transform groundCheck_CR;
    [SerializeField]
    Transform groundCheck_L;
    [SerializeField]
    Transform groundCheck_R;

    public float runSpeed;
    public float jumpSpeed;
    
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
        
        if (Physics2D.Linecast(transform.position, groundCheck_C.position, 1 << LayerMask.NameToLayer("Ground"))) 
             isGrounded_C= true;
        else isGrounded_C = false;

        if (Physics2D.Linecast(transform.position, groundCheck_CL.position, 1 << LayerMask.NameToLayer("Ground")))
            isGrounded_CL = true;
        else isGrounded_CL = false;
        
        if (Physics2D.Linecast(transform.position, groundCheck_CR.position, 1 << LayerMask.NameToLayer("Ground")))
            isGrounded_CR = true;
        else isGrounded_CR = false;

        if (Physics2D.Linecast(transform.position, groundCheck_L.position, 1 << LayerMask.NameToLayer("Ground")))
            isGrounded_L = true;
        else isGrounded_L = false;

        if (Physics2D.Linecast(transform.position, groundCheck_R.position, 1 << LayerMask.NameToLayer("Ground")))
            isGrounded_R = true;
        else isGrounded_R = false;

        ySpeed = rb.velocity.y;
        if (hasJetPack)
        {
            JetPackMovement();
        }
        else {
            //IS ON THE GROUND
            if (isGrounded_C && (isGrounded_L || isGrounded_R))
            {
                OnGroundMovement();
            }
            else
            //IS ON THE WALL
            if(!isGrounded_C && (isGrounded_L || isGrounded_R))
            {
                OnWallMovement();
            }
            //IS ON THE AIR
            else
            {
                OnAirMovement();
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

    private void OnAirMovement()
    {
        if (rb.velocity.y < 0)
            animator.Play("fall_animation");
        else
            animator.Play("jump_animation");


        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (Input.GetKey("space") && (rb.velocity.y < jumpSpeed / 2) && doubleJump)
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
    private void OnGroundMovement()
    {
        if (canDoubleJump)
            doubleJump = true;

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (Input.GetKey("space"))
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
            if (Input.GetKey("space"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                animator.Play("jump_animation");
            }
            else
            {
                rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
                animator.Play("animation_run");
                spriteRenderer.flipX = true;
            }
        }
        else
        if (Input.GetKey("space"))
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

    private void OnWallMovement()
    {
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (Input.GetKey("space") && (rb.velocity.y < jumpSpeed / 2) && doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * .7f);
                animator.Play("jump_animation");
                doubleJump = false;
            }
            else
            {
                if (rb.velocity.y == 0)
                    rb.velocity = new Vector2(runSpeed, -jumpSpeed);     //alterar este -jumpSpeed para aceleraçao da gravidade ( aka, cair como se estivesse no ar normalmente)
                else
                    rb.velocity = new Vector2(runSpeed, rb.velocity.y); 
                animator.Play("fall_animation");
            }
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
            {
                if (rb.velocity.y == 0)// -jumpSpeed
                    rb.velocity = new Vector2(-runSpeed, -jumpSpeed);   //alterar este -jumpSpeed para aceleraçao da gravidade ( aka, cair como se estivesse no ar normalmente)
                else
                    rb.velocity = new Vector2(-runSpeed, rb.velocity.y);
                animator.Play("fall_animation");
            }
            spriteRenderer.flipX = true;
        }
    }
    private void JetPackMovement()
    {

    }
}
