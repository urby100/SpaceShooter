using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3ProjectileController : MonoBehaviour
{
    public GameObject player;

    public float followAfterXSeconds = 0.2f;
    public float movementSpeed = 5f;
    float timeToFollow;

    float rotationSpeed = 3f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeToFollow = Time.time + followAfterXSeconds;
    }

    void FixedUpdate()
    {
        if (Time.time > timeToFollow)
        {
            Vector2 vectorToTarget = player.transform.position - transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        rb.velocity = transform.up * movementSpeed;
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
