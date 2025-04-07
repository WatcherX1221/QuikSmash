using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float playerSpeed = 7f;
    public float playerJump = 10f;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    public bool isGrounded;
    public static int xAim = 0;
    public static int yAim = 0;

    public float slamStallTimer = 0f;
    public float slamStallTime = 0.25f;
    public bool slamStalled;
    public Vector2 addedForce;
    // Start is called before the first frame update
    void Start()
    {
        xAim = 0;
        yAim = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        isGrounded = false;
        slamStallTimer = 0f;
        slamStalled = false;
        addedForce = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(addedForce.x >= 0)
        {
            addedForce = new Vector2(addedForce.x - Time.deltaTime, 0);
        }
        else if(addedForce.x <= 0)
        {
            addedForce = new Vector2(0, addedForce.y);
        }
        
        if(addedForce.y >= 0)
        {
            addedForce = new Vector2(0, addedForce.y - Time.deltaTime);
        }
        else if(addedForce.y <= 0)
        {
            addedForce = new Vector2(addedForce.x, 0);
        }
        

        float movement = Input.GetAxis("Horizontal");
        float movespeed = movement * playerSpeed;
        rb.velocity = new Vector2(movespeed + addedForce.x, rb.velocity.y + addedForce.y);

        if(rb.velocity.x < 0)
        {
            sprite.flipX = true;
        }
        else if(rb.velocity.x > 0)
        {
            sprite.flipX = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(new Vector2(0f, playerJump), ForceMode2D.Impulse);
        }

        if(rb.velocity.x != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        

        if(slamStalled)
        {
            slamStallTimer += Time.deltaTime;
            rb.velocity = new Vector2(movespeed * 0.5f, 2);
            if(slamStallTimer >= slamStallTime)
            {
                rb.AddForce(new Vector2(0f, -5f), ForceMode2D.Impulse);
                slamStalled = false;
            }
            
        }
        else if(!slamStalled)
        {
            if(Input.GetKeyDown(KeyCode.S))
            {   
                slamStalled = true;
                slamStallTimer = 0f;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Solid")
        {
            isGrounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Solid")
        {
            isGrounded = false;
        }
    }


}
