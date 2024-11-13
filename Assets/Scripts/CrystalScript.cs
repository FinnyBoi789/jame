using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    int score;
    public GameObject crystal;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        ScoreScript1.score += 1;
        gone();
    }

    void gone()
    {
        Destroy(crystal);
    }
}
