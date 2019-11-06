using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_ProjectileController : MonoBehaviour
{
    public float bulletSpeed = 10f;
    Rigidbody2D rb;
    float destroyDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
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
