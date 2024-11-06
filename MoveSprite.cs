using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{
    //declares keybinds
    public string up;
    public string down;
    public string left;
    public string right;

    //boolean for if the player is facing right or not
    private bool isFacingRight = true;

    //wall sliding
    private bool isWallSliding;
    private float wallSlideSpeed = 2f;

    //wall jumping
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    //player speed and declares horizontal variable to allow movement
    private float speed = 10f;
    private float horizontal; 

    //number of jumps variable so that multiple jumps cannot happen
    public int numJumps = 1;

    public Rigidbody2D rb;
    public Transform wallCheck;
    public LayerMask wallLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(up) && numJumps > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 10, 0);
            numJumps -= 1;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        this.transform.Translate(move);

        wallSlide();
        wallJump();

        if (isWallJumping == false)
        {
            flip();
        }
    }

    void FixedUpdate()
    {
        if (isWallJumping == false)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            numJumps = 1;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

        }
    }

    void flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void wallSlide()
    {
        if(isWalled() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x / 10, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void wallJump()
    {
        if (isWallSliding == true)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(stopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(up) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;
            
            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = false;
                Vector2 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
        Invoke(nameof(stopWallJumping), wallJumpingDuration);
    }

    private void stopWallJumping()
    {
        isWallJumping = false;
    }
}
