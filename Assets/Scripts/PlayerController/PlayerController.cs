using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject feet;
    public float moveSpeed;
    public float jumpForce;
    public float lerpSpeedIncrease = 1;
    public float lerpSpeedDecrease;
    public float jumpForceMin;
    public float jumpForceMax;
    bool spaceHeldDown;

    float fallMultiplier = 2.5f;
    float lowJumpMultiplier = 2f;

    public bool grounded;
    public float radiusCircle;
    public float mayJump;
    public float mayJumpTime = 0.1f;
    public UIJumpBar uiJumpBar;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        mayJump = mayJumpTime;
        rb = GetComponent<Rigidbody2D>();
        uiJumpBar.GetCurrentFill(jumpForce, jumpForceMin,jumpForceMax);
        anim = GetComponent<Animator>();
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
        float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position += new Vector3(horizontalMove, 0, 0);

        if (horizontalMove > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (horizontalMove < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

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
            if (jumpForce >= jumpForceMax)
            {
                jumpForce = jumpForceMax;
                return;
            }

            UpdateUiBar(jumpForceMax, lerpSpeedIncrease);
        }
        else
        { 
            UpdateUiBar(jumpForceMin, lerpSpeedDecrease);
        }

        //after charge up, add up force to the players velocity, and set the UI bar back to 0
        if (Input.GetKeyUp(KeyCode.Space) && grounded)
        {
            anim.SetTrigger("Jumps");
            rb.velocity = Vector2.up * jumpForce;
           // jumpForce = 5;
            //uiJumpBar.GetCurrentFill(jumpForce, jumpForceMin, jumpForceMax);
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
        }else if(colliders.Length <= 0)
        {
            //allows for a tiny window of time to still be able to jump once leaving the collider
            mayJump -= Time.fixedDeltaTime;
            if(mayJump <=0)
            {
                grounded = false;
            }
        }
    }

    void UpdateUiBar(float desiredNumber, float lerpSpeed)
    {
        jumpForce = Mathf.MoveTowards(jumpForce, desiredNumber, Time.deltaTime * lerpSpeed);
        uiJumpBar.GetCurrentFill(jumpForce, jumpForceMin, jumpForceMax);
    }
}
