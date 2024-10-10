using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{

    public string up;
    public string down;
    public string left;
    public string right;

    public int numJumps = 1;

    public Rigidbody2D rb;

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



        if (Input.GetKey(down))
        {
            move = new Vector3(0, -0.03f, 0);
        }

        if (Input.GetKey(left))
        {
            move = new Vector3(-0.03f, 0, 0);
        }

        if (Input.GetKey(right))
        {
            move = new Vector3(0.03f, 0, 0);
        }

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
