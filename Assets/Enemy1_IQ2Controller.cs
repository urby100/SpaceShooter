using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_IQ2Controller : MonoBehaviour
{
    float health = 100f;
    float damageTakeAmount = 30f;
    float damageTickDelay = 0.03f;
    float damageTickTime;

    public GameObject player;

    Rigidbody2D rb;
    float movementSpeed = 13;


    float restrictHorizontal = 12f;
    float restrictVertical = 7f;

    float rotationSpeed = 1f;


    // Start is called before the first frame update

    void Start()
    {
        gameObject.name = "Enemy1_IQ2";
        rb = GetComponent<Rigidbody2D>();
        setPath();
    }

    void FixedUpdate()
    {
        if (transform.position.x >= restrictHorizontal ||
            transform.position.x <= -restrictHorizontal ||
            transform.position.y >= restrictVertical ||
            transform.position.y <= -restrictVertical)
        {
            setPath();
        }
        //rotate towards player slowly
        Vector2 vectorToTarget = player.transform.position - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x)*Mathf.Rad2Deg)-90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        
        rb.velocity = transform.up * movementSpeed;

    }
    void setPath()
    {
        rb.velocity = Vector2.zero;
        Vector2 direction = new Vector2(
            player.transform.position.x - transform.position.x,
            player.transform.position.y - transform.position.y);
        transform.up = direction;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectiles"))
        {
            //Destroy(collision.gameObject);
            health -= damageTakeAmount;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectiles"))
        {
            if (Time.time > damageTickTime)
            {
                health -= damageTakeAmount;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
                damageTickTime = Time.time + damageTickDelay;
            }
            //Destroy(collision.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
