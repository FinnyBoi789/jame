using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{

    public string up;
    public string down;
    public string left;
    public string right;

    private float speed = 10f;
    private float horizontal;

    public int numJumps = 1;

    public Rigidbody2D rb;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        Vector3 move = new Vector3(0, 0, 0);

        if (Input.GetKeyDown(up) && numJumps > 0) 
        {   
            rb.velocity = new Vector3(rb.velocity.x, 10, 0);
            numJumps -= 1;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        this.transform.Translate(move);

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
}
