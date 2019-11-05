using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float health = 100;

    public float shield = 100;

    float damageAmount = 10;

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

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (shield > 0)
            {
                float difference = shield - damageAmount;
                if (difference < 0)//npr. diff = sh[5] - dmgAmnt[10] = -5... 
                {
                    shield = 0;
                    health += difference;//vzamemo -5 od healtha...
                }
                else
                {
                    shield -= damageAmount;
                }
            }
            else
            {
                health -= damageAmount;//
            }
            Debug.Log(health + " , " + shield);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (shield > 0)
            {
                float difference = shield - damageAmount;
                if (difference < 0)//npr. diff = sh[5] - dmgAmnt[10] = -5... 
                {
                    shield = 0;
                    health += difference;//vzamemo -5 od healtha...
                }
                else
                {
                    shield -= damageAmount;
                }
            }
            else
            {
                health -= damageAmount;//
            }
            Debug.Log(health + " , " + shield);
        }
    }
}
