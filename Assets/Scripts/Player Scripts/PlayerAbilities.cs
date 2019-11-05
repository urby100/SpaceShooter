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

    public GameObject attack2SpawnPoint;
    public Transform attack2HitPoint;
    private LineRenderer attack2LineRenderer;
    BoxCollider2D attack2LineCollider;


    float speedMultiplier = 2;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attack2LineRenderer = attack2SpawnPoint.GetComponent<LineRenderer>();
        attack2LineRenderer.enabled = false;
        attack2LineRenderer.useWorldSpace = true;

        attack2LineCollider = attack2SpawnPoint.GetComponent<BoxCollider2D>();
        attack2LineCollider.enabled = false;
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

        //line rendered
        RaycastHit2D hit = Physics2D.Raycast(attack2SpawnPoint.transform.position, attack2SpawnPoint.transform.up);
        attack2HitPoint.position = attack2SpawnPoint.transform.up * 20 + attack2SpawnPoint.transform.position;
        attack2LineRenderer.SetPosition(0, attack2SpawnPoint.transform.position);
        attack2LineRenderer.SetPosition(1, attack2HitPoint.position);

        //line collider



        if (Input.GetKey(KeyCode.Mouse1))
        {
            attack2LineCollider.enabled = true;
            attack2LineRenderer.enabled = true;
        }
        else
        {
            attack2LineCollider.enabled = false;
            attack2LineRenderer.enabled = false;
        }
    }

}
