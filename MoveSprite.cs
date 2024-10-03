using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSprite : MonoBehaviour
{

    public string up;
    public string down;
    public string left;
    public string right;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(0, 0, 0);

        if (Input.GetKey(up))
        {
            move = new Vector3(0, 0.03f, 0);
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
}
