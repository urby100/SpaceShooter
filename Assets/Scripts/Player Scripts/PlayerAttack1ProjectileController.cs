using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1ProjectileController : MonoBehaviour
{
    float bulletSpeed = 20f;
    Rigidbody2D rb;
    float destroyDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "PlayerAttack1Projectile";
        Destroy(gameObject, destroyDelay);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
