using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameManager GM;
    PlayerAbilities playerAbilitiesScript;
    public bool invincible;
    float invincibleLasts = 4f;
    float invincibleTime;

    public float health = 100;
    float healthRegenTick = 3;

    public float shield = 100;
    float shieldRegenTick = 3;

    float damageTakeAmount = 10;
    float damageTickDelay = 0.05f;
    float damageTickTime;
    
    public bool invulnerableCheatCode = false;



    // Start is called before the first frame update
    void Start()
    {
        playerAbilitiesScript = GetComponent<PlayerAbilities>();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            invulnerableCheatCode = !invulnerableCheatCode;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (health <= 0) {
            return;
        }



        health = Mathf.Clamp(health + (healthRegenTick * Time.deltaTime), 0, 100);
        shield = Mathf.Clamp(shield + (shieldRegenTick * Time.deltaTime), 0, 100);
        if (Time.time > invincibleTime)
        {
            invincible = playerAbilitiesScript.shieldAttackStart;
        }
        if (invulnerableCheatCode) {
            invincible = true;
        }
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

            shield = Mathf.Clamp(shield + powerUp.shield, 0, 100);

            playerAbilitiesScript.laserFuel = Mathf.Clamp(playerAbilitiesScript.laserFuel + powerUp.laserFuel, 0, 100);
            playerAbilitiesScript.nitroFuel = Mathf.Clamp(playerAbilitiesScript.nitroFuel + powerUp.nitroFuel, 0, 100);
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
    private void OnCollisionStay2D(Collision2D collision)
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
        GM.resetScoreMultiplier();
    }
}
