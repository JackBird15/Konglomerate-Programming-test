﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed;
    public float xClamp;

    [Header("Jump Variables")]
    public float jumpForce;
    public float jumpForceMin;
    public float jumpForceMax;
    public float lerpSpeedIncrease = 1;
    public float lerpSpeedDecrease;

    [Header("Gravity Modifiers")]
    float fallMultiplier = 2.5f;
    float lowJumpMultiplier = 2f;

    [Header("GroundCheck")]
    public GameObject feet;
    public bool grounded;
    public float radiusCircle;
    public float mayJump;
    public float mayJumpTime = 0.1f;
    public UIJumpBar uiJumpBar;
   public bool jump;
    Animator anim;
    public AudioManager audioManager;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        //grabbing all components from the GO
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //set our mayjump time
        mayJump = mayJumpTime;
        //setting Jump UI bar
        uiJumpBar.GetCurrentFill(jumpForce, jumpForceMin,jumpForceMax, false);
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Movement();
    }

    private void FixedUpdate()
    {
        GroundCheck();
    }

    void Movement()
    {
        //Cant run off the side
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -xClamp, xClamp),
           transform.position.y, transform.position.z);

        //grabbing Axis so A=-1 and D = 1
        float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(horizontalMove, 0, 0);

        //rotates 2D sprite to face correct way
        if (horizontalMove > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (horizontalMove < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //If not moving, set Anim to Idle
        if (horizontalMove != 0)
        {
            anim.SetBool("Run", true);
        } else { anim.SetBool("Run", false); }
    }

    void Jump()
    {
        //hold down to increase jump force and UI bar
        //if jump force has reached maximum it will not peak any higher
        if (Input.GetKey(KeyCode.Space) && grounded)
        {

            anim.SetBool("Prejump", true);
            if (jumpForce >= jumpForceMax)
            {
                jumpForce = jumpForceMax;
                return;
            }
            UpdateUiBar(jumpForceMax, lerpSpeedIncrease, true);
        }
        else
        {
            anim.SetBool("Prejump", false);
            UpdateUiBar(jumpForceMin, lerpSpeedDecrease, false);
        }

        //after charge up, add up force to the players velocity, and set the UI bar back to 0
        if (Input.GetKeyUp(KeyCode.Space) && grounded)
        {
            jump = true;
            anim.SetBool("Prejump", false);
            anim.SetTrigger("Jumps");
            audioManager.Play("Jump");
            rb.velocity = Vector2.up * jumpForce;
            print(rb.velocity);
        }

        //increasing gravity over time to make the player feel less floaty
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        } else if (rb.velocity.y > 0 && !Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void GroundCheck()
    {
        //bit shift the index of the layer to get a bit mask
        //layer8 = player
        int layerMask = 1 << 8;

        //~ makes it raycast everything except layer 8
        //check for any colliders that come in contact with the overlap sphere
        Collider2D[] colliders = Physics2D.OverlapCircleAll(feet.transform.position, radiusCircle, ~layerMask);
        if (colliders.Length > 0)
        {
            grounded = true;
            mayJump = mayJumpTime;
        } else if (colliders.Length <= 0)
        {
            if (!jump)
            {
                //allows for a tiny window of time to still be able to jump once leaving the collider
                mayJump -= Time.fixedDeltaTime;

                if (mayJump <= 0)
                {
                    grounded = false;
                    jump = false;
                    print("MayJump");
                }
            }else if (jump)
            {
                grounded = false;
                print("NOITMayJump");
                jump = false;
            }
        }
    }

    //Function to update Jumpbar
    void UpdateUiBar(float desiredNumber, float lerpSpeed, bool enlarge)
    {
        jumpForce = Mathf.MoveTowards(jumpForce, desiredNumber, Time.deltaTime * lerpSpeed);
        uiJumpBar.GetCurrentFill(jumpForce, jumpForceMin, jumpForceMax, enlarge);
    }
}
