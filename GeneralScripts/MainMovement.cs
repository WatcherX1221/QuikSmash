using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMovement : MonoBehaviour
{
    public float baseSpeed = 10;
    public float maxSpeed = 17;
    public float accelerationTime = 3;
    public float accelerationCharge;
    public float jumpPower = 10;
    public bool canJump;
    public bool canSlam;
    Rigidbody2D rb;
    public InputAction move;
    public InputAction jump;
    InputAction slam;
    InputAction ability1;
    float slamCharge = 0f;
    float slamChargeTime = 0.25f;
    [SerializeField]
    float directionSwapTimer;

    //Ability variables
    AbilityFunctions abilities;
    public float ability1Cooldown;
    public float ability2Cooldown;
    public float ability3Cooldown;

    void Start()
    {
        // Object referencing
        rb = GetComponent<Rigidbody2D>();
        abilities = GetComponent<AbilityFunctions>();
        
        // Value defining
        accelerationCharge = 0;
        canJump = true;
        canSlam = false;
        directionSwapTimer = 0;
        ability1Cooldown = 0;
        ability2Cooldown = 0;
        ability3Cooldown = 0;

        // Input setting

        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        slam = InputSystem.actions.FindAction("Slam");
        ability1 = InputSystem.actions.FindAction("Ability1");
    }

    // Update is called once per frame
    void Update()
    {
        TimerFunction();
        // Track move values
        Vector2 movementVector = move.ReadValue<Vector2>();
        int movement = (int)movementVector.x;
        if (Mathf.Abs(movement) > 0)
        {
            accelerationCharge += Time.deltaTime;
            rb.linearVelocityX = baseSpeed / movement;
            directionSwapTimer = 0.5f;
            if (accelerationCharge >= accelerationTime)
            {
                rb.linearVelocityX = maxSpeed / movement;
            }
        }
        else if (Mathf.Abs(movement) <= 0 && directionSwapTimer > 0)
        {
            directionSwapTimer -= Time.deltaTime;
        }
        else
        {
            accelerationCharge = 0;
        }


        if (jump.IsPressed() && canJump)
        {
            rb.linearVelocityY = jumpPower;
        }
        else
        {
            rb.linearVelocityY -= Time.deltaTime;
        }

        if (slam.IsPressed() && canSlam)
        {
            if (movement != 0)
            {
                rb.linearVelocityX = baseSpeed * 0.5f / movement;
            }
            else
            {
                rb.linearVelocityX *= 0.5f;
            }
            Debug.Log("slamCharge = " + slamCharge);
            if (slamCharge <= slamChargeTime)
            {
                slamCharge += Time.deltaTime;
            }
            else
            {
                rb.linearVelocity = new Vector2(0f, -jumpPower * 2);
                slamCharge = 0f;
                canSlam = false;
            }
        }
    }
    
    void TimerFunction()
    {
        // Function handles all timers
        if (ability1Cooldown > 0)
        {
            ability1Cooldown -= Time.deltaTime;
        }
        if (ability2Cooldown > 0)
        {
            ability2Cooldown -= Time.deltaTime;
        }
        if (ability3Cooldown > 0)
        {
            ability3Cooldown -= Time.deltaTime;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            canJump = true;
            canSlam = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            canJump = false;
            canSlam = true;
        }
    }

}
