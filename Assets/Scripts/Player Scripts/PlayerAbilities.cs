using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject attack1Prefab;
    
    public Transform attack1SpawnPoint;
    float attackSpeedAttack1 = 0.1f;
    float attack1NextAttack;

    public GameObject attack2SpawnPoint;
    public Transform attack2HitPoint;
    private LineRenderer attack2LineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        attack2LineRenderer = attack2SpawnPoint.GetComponent<LineRenderer>();
        attack2LineRenderer.enabled = false;
        attack2LineRenderer.useWorldSpace = true;
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (Time.time > attack1NextAttack) {
                GameObject projectile = Instantiate(attack1Prefab, attack1SpawnPoint.position, transform.rotation);
                attack1NextAttack = Time.time + attackSpeedAttack1;
            }
        }
        RaycastHit2D hit = Physics2D.Raycast(attack2SpawnPoint.transform.position, attack2SpawnPoint.transform.up);
        attack2HitPoint.position = attack2SpawnPoint.transform.up *20 + attack2SpawnPoint.transform.position;
        attack2LineRenderer.SetPosition(0, attack2SpawnPoint.transform.position);
        attack2LineRenderer.SetPosition(1, attack2HitPoint.position);
        if (Input.GetKey(KeyCode.Mouse1))
        {
            attack2LineRenderer.enabled = true;
        }
        else
        {
            attack2LineRenderer.enabled = false;
        }
    }

}
