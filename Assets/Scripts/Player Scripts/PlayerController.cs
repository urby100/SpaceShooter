using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameManager GM;
    public bool invincible;
    float invincibleLasts = 4f;
    float invincibleTime;

    public float health = 100;

    public float shield = 100;

    float damageTakeAmount = 10;
    float damageTickDelay = 0.05f;
    float damageTickTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > invincibleTime)
        {
            invincible = false;
        }
        //Debug.Log(health + " " + shield + " " + invincible);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")
            || collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectiles"))
        {
            takeDamage();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("PowerUp"))
        {
            PowerUpController powerUp = collision.gameObject.GetComponent<PowerUpController>();
            health = Mathf.Clamp(health + powerUp.health, 0, 100);
            if (powerUp.health>0) {
                GM.updateHealthBar();
            }

            shield = Mathf.Clamp(shield + powerUp.shield, 0, 100);
            if (powerUp.shield > 0) {

                GM.updateShieldBar();
            }
            if (powerUp.invincible)
            {
                invincible = true;
                invincibleTime = Time.time + invincibleLasts;
            }
            GM.setNoDmgMultiplier(powerUp.scoreMultiplier);
            Destroy(collision.gameObject);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") 
            || collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")
            || collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectiles"))
        {
            takeDamage();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")
            || collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectiles")
            || collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            if (Time.time > damageTickTime)
            {
                takeDamage();
                damageTickTime = Time.time + damageTickDelay;
            }
        }

    }
    void takeDamage()
    {
        if (invincible)
        {
            return;
        }
        if (shield > 0)
        {
            float difference = shield - damageTakeAmount;
            if (difference < 0)//npr. diff = sh[5] - dmgAmnt[10] = -5... 
            {
                shield = 0;
                health += difference;//vzamemo -5 od healtha...
            }
            else
            {
                shield -= damageTakeAmount;
            }
        }
        else
        {
            health -= damageTakeAmount;//
        }
        health = Mathf.Clamp(health, 0, 100);
        GM.updateHealthBar();
        GM.updateShieldBar();
    }
}
