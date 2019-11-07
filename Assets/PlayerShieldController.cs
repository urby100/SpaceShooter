using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldController : MonoBehaviour
{
    public float powerMultiplier=1f;
    float minPower = 1f;
    float maxPower = 3f;
    float powerTick = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")
            || collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")
            || collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectiles"))
        {
            powerMultiplier = Mathf.Clamp(powerMultiplier + powerTick,minPower,maxPower);

        }
    }

}
