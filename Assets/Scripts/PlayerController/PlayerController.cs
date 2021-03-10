using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action<float, float, float, bool> UpdateUI;
    public static event Action<string> PlayAudio;

    Rigidbody2D rb;
    public float moveSpeed;
    public Vector2 invisibleWallsLocation;

    [Header("Jump Variables")]
    public float jumpForce;
    public float jumpForceMin;
    public float jumpForceMax;
    private bool enlarge;

    public float lerpSpeedIncrease = 1;
    public float lerpSpeedDecrease;
    private float timer;
    private bool jump;
    private bool jumpKey;
    private bool jumpHold;

    [Header("Gravity Modifiers")]
    float fallMultiplier = 2.5f;
    float lowJumpMultiplier = 2f;

    [Header("GroundCheck")]
    public Vector2 feetLocation = new Vector2(0,-0.36f);
    public bool grounded;
    public float radiusCircle;
    public float mayJumpTime = 0.1f;
    private float mayJump;
    
    Animator anim;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        print("YeET");
        //grabbing all components from the GO

        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        else { Debug.LogError("PLEASE ATTACH RIGIDBODY2D"); }

        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("PLEASE ATTACH ANIMATOR");
            return;
        }
        //set our mayjump time
        mayJump = mayJumpTime;
    }

    // Update is called once per frame
    private void Update()
    {
        JumpControls();
        Movement();
        UpdateUI?.Invoke(jumpForce, jumpForceMin, jumpForceMax, enlarge);
    }

    private void FixedUpdate()
    {
        Jump();
        GroundCheck();
    }

    private void Movement()
    {

        //Cant run off the side
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -invisibleWallsLocation.x, invisibleWallsLocation.x),
           Mathf.Clamp(transform.position.y, -invisibleWallsLocation.y, invisibleWallsLocation.y), transform.position.z);

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
        if (horizontalMove != 0 && anim !=null)
        {
            anim.SetBool("Run", true);
        } else { anim.SetBool("Run", false); }
    }

    //Since Physics should always go in Fixed update, the actual inputs still need to be in update, 
    //or else you may be a frame or 2 out when clicking a button
    //The KeyUp function almost never lands when in fixedUpdate, so a bool is triggered here, and then set to false in fixedupdate when the function is called
    private void JumpControls()
    {
        if (Input.GetKeyUp(KeyCode.Space) && grounded)
        {
            jumpKey = true;
        }
       

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            jumpHold = true;
        }
        else
        {
            jumpHold = false;
        }
    }

    private void Jump()
    {
        //hold down to increase jump force and UI bar
        if (jumpHold)
        {
            //enlarge the UI
            enlarge = true;

            if (anim != null)
            anim.SetBool("Prejump", true);

            //if jump force has reached maximum it will not peak any higher
            if (jumpForce >= jumpForceMax)
            {
                jumpForce = jumpForceMax;
                return;
            }
            LerpJump(jumpForceMax, lerpSpeedIncrease);

            //if player taps the space bar, the jumpforce will always be 5
            timer += Time.fixedDeltaTime;
            if (timer <= 0.1f)
            {
                jumpForce = jumpForceMin;
            }
        }
        else
        {
            //stop enlarging the UI
            enlarge = false;

            if (anim != null)
                anim.SetBool("Prejump", false);

            LerpJump(jumpForceMin, lerpSpeedDecrease);
        }

        //after charge up, add up force to the players velocity, and set the UI bar back to 0
        if (jumpKey)
        {
            jump = true;

            if (anim != null)
            {
                anim.SetBool("Prejump", false);
                anim.SetTrigger("Jumps");
            }

            PlayAudio?.Invoke("Jump");
            rb.velocity = Vector2.up * jumpForce;
            timer = 0f;
            jumpKey = false;
        }

        //increasing gravity over time to make the player feel less floaty
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void GroundCheck()
    {
        //bit shift the index of the layer to get a bit mask
        //layer8 = player
        int layerMask = 1 << 8;

        //~ makes it raycast everything except layer 8
        //check for any colliders that come in contact with the overlap sphere
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y+ feetLocation.y), radiusCircle, ~layerMask);
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
                }
            }else if (jump)
            {
                grounded = false;
                jump = false;
            }
        }
    }

    //Function to Lerp the Jump
    private void LerpJump( float desiredNumber, float lerpSpeed)
    {
        jumpForce = Mathf.MoveTowards(jumpForce, desiredNumber, Time.deltaTime * lerpSpeed);
    }
}
