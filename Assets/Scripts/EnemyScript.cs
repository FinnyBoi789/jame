using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;

    public GameObject death;

    public GameObject enemyPrefab;

    public ParticleSystem explode;

    void Start()
    {
        explode.Stop();
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StartCoroutine(Cooldown());
            Explode();
        }


    }
    void die()
    {
        Destroy(gameObject);
        explode.transform.parent = null;
        Score.playerScore += 10;
    }

    void Explode()
    {
        explode.Play();
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.01f);
        die();
    }
}