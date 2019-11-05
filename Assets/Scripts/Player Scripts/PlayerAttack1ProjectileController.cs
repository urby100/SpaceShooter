using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1ProjectileController : MonoBehaviour
{
    float bulletSpeed = 15f;
    float decreaseSpeed = 0.9f;
    Rigidbody2D rb;
    float destroyDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyDelay);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity=transform.up*bulletSpeed;
    }

    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
