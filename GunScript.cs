using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject cube;

    bool cooldown = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !cooldown){
            shoot();
        }
    }

    void shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.4f);
        cooldown = false;
    }
}
