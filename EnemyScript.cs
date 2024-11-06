using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health = 100;

    public GameObject death;

    public GameObject enemyPrefab;

    void Start()
    {
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            die();
        }

        void die()
        {
            Instantiate(death, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
