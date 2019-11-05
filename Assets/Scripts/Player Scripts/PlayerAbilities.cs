using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public GameObject attack1Prefab;
    
    public Transform attack1SpawnPoint;
    float attackSpeedAttack1 = 0.1f;
    float attack1NextAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0)) {
            if (Time.time > attack1NextAttack) {
                GameObject projectile = Instantiate(attack1Prefab, attack1SpawnPoint.position, transform.rotation);
                attack1NextAttack = Time.time + attackSpeedAttack1;
            }
        }
    }
}
