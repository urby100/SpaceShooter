using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool restrictMove = false;

    Rigidbody2D rb;

    float movementSpeed = 10;
    float restrictHorizontal = 9.6f;
    float restrictVertical = 5.4f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (restrictMove) {
            return;
        }
        //keyboard controls
        float verticalMovement = Input.GetAxis("Vertical");
        float horizontalMovement = Input.GetAxis("Horizontal");
        //Debug.Log(horizontalMovement +", " +verticalMovement);
        //movement restrict
        Vector2 colliderSize = GetComponent<BoxCollider2D>().size;
        if (transform.position.x >= (restrictHorizontal - colliderSize.x) && horizontalMovement > 0)
        {
            horizontalMovement = 0;
        }
        if (transform.position.x <= (-restrictHorizontal + colliderSize.x) && horizontalMovement < 0)
        {
            horizontalMovement = 0;
        }
        if (transform.position.y >= (restrictVertical - colliderSize.y) && verticalMovement > 0)
        {
            verticalMovement = 0;
        }
        if (transform.position.y <= (-restrictVertical + colliderSize.y) && verticalMovement < 0)
        {
            verticalMovement = 0;
        }

        Vector2 moveVector = new Vector2(horizontalMovement, verticalMovement);

        rb.velocity = moveVector * movementSpeed;

        //mouse controls
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //ko dva vektorja odšteješ dobiš smer
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);
        transform.up = direction;
    }
}
