using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle1Controller : MonoBehaviour
{
    public Vector2 moveVector;

    Rigidbody2D rb;

    float minSize = 0.3f;
    float maxSize = 1f;
    float size;

    float minMovementSpeed = 1f;
    float maxMovementSpeed = 4f;
    float movementSpeed;

    float rotationDirection;
    float minRotationSpeed = 80f;
    float maxRotationSpeed = 150f;
    float rotationSpeed;


    float restrictHorizontal = 12f;
    float restrictVertical = 8f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Obstacle1";
        rb = GetComponent<Rigidbody2D>();

        movementSpeed = Random.Range(minMovementSpeed,maxMovementSpeed);


        size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, size);

        rotationDirection = Random.Range(0, 2) * 2 - 1;
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        rb.AddTorque(rotationSpeed * rotationDirection, ForceMode2D.Impulse);

    }

    void FixedUpdate()
    {
        rb.velocity = moveVector * movementSpeed;

        //movement restrict
        if (transform.position.x >= restrictHorizontal || transform.position.x <= -restrictHorizontal
            || transform.position.y >= restrictVertical || transform.position.y <= -restrictVertical)
        {
            Destroy(gameObject);
        }
    }
}
