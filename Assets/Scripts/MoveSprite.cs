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
    public bool isFacingRight = true;

    //wall sliding
    private bool isWallSliding;
    private float wallSlideSpeed = 5f;

    //wall jumping
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    //dashing
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    //player speed and declares horizontal variable to allow movement
    private float speed = 10f;
    private float horizontal;

    //number of jumps variable so that multiple jumps cannot happen
    public int numJumps = 1;

    public Rigidbody2D rb;
    public Transform wallCheck;
    public LayerMask wallLayer;
    public TrailRenderer trailRenderer;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isDashing)
        {
            return;
        }

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

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true)
        {
            StartCoroutine(dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

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

        if (!isFacingRight)
        {
            bullet.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            bullet.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void wallSlide()
    {
        if (isWalled())
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x / 2, -wallSlideSpeed);
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

        if (Input.GetKeyDown(up) && wallJumpingCounter > 0f)
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

    private IEnumerator dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}