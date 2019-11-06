using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject playerProjectiles;

    public GameObject attack1Prefab;
    public Transform attack1SpawnPoint;
    float attackSpeedAttack1 = 0.07f;
    float attack1NextAttack;

    public GameObject attack2Laser;


    float speedMultiplier = 2;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attack2Laser.SetActive(false);
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = rb.velocity * speedMultiplier;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Time.time > attack1NextAttack)
            {
                GameObject projectile = Instantiate(attack1Prefab, attack1SpawnPoint.position, transform.rotation);
                projectile.transform.parent = playerProjectiles.transform;
                attack1NextAttack = Time.time + attackSpeedAttack1;
            }
        }
        

        if (Input.GetKey(KeyCode.Mouse1))
        {
            attack2Laser.SetActive(true);
        }
        else
        {
            attack2Laser.SetActive(false);
        }
    }

}
