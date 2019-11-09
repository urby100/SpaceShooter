using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public bool restrictAbilities = false;
    public GameObject playerShieldAttackPrefab;

    Rigidbody2D rb;

    public GameObject playerProjectiles;

    public GameObject attack1Prefab;
    public Transform attack1SpawnPoint;
    float attackSpeedAttack1 = 0.07f;
    float attack1NextAttack;

    public GameObject attack2Laser;
    public GameObject shield;
    public GameObject nitroFlame;
    float nitroFlameMinSize = 0.9f;
    float nitroFlameMaxSize = 1.5f;



    float speedMultiplier = 2;
    public float nitroFuel = 100f;
    float nitroMinFuel = 0f;
    float nitroMaxFuel = 100f;
    float nitroWasteTick = 40f;
    float nitroRegenTick = 18f;
    bool nitroCooldown = false;
    float nitroMinFuelToUseAfterCooldown = 20;


    public float laserFuel = 100f;
    float laserMinFuel = 0f;
    float laserMaxFuel = 100f;
    float laserWasteTick = 80f;
    float laserRegenTick = 12f;
    bool laserAttackCooldown = false;
    float laserMinFuelToAttackAfterCooldown = 20;



    PlayerController playerCont;
    public bool shieldAttackStart = false;
    float shieldWasteTick = 100;
    float minShieldToAttack = 25;


    // Start is called before the first frame update
    void Start()
    {
        playerCont = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        attack2Laser.SetActive(false);
    }
    void FixedUpdate()
    {
        nitroFuel = Mathf.Clamp(nitroFuel + (nitroRegenTick * Time.deltaTime), nitroMinFuel, nitroMaxFuel);
        Vector3 nitroFlameSize = nitroFlame.transform.localScale;
        nitroFlameSize.y = nitroFlameMaxSize - ((nitroMaxFuel - nitroFuel) / 100) * (nitroFlameMaxSize - nitroFlameMinSize);
        nitroFlame.transform.localScale = nitroFlameSize;


        laserFuel = Mathf.Clamp(laserFuel + (laserRegenTick * Time.deltaTime), laserMinFuel, laserMaxFuel);

        if (Input.GetKey(KeyCode.LeftShift) && !nitroCooldown && !restrictAbilities)
        {
            nitroFuel = Mathf.Clamp(nitroFuel - (nitroWasteTick * Time.deltaTime), nitroMinFuel, nitroMaxFuel);
            if (nitroFuel != 0)
            {
                rb.velocity = rb.velocity * speedMultiplier;
                nitroFlame.SetActive(true);
            }
            else
            {
                nitroCooldown = true;
            }
        }
        else
        {
            nitroFlame.SetActive(false);
            if (nitroFuel > nitroMinFuelToUseAfterCooldown)
            {
                nitroCooldown = false;
            }
        }
        if (Input.GetKey(KeyCode.Mouse0) && !restrictAbilities)
        {
            if (Time.time > attack1NextAttack)
            {
                GameObject projectile = Instantiate(attack1Prefab, attack1SpawnPoint.position, transform.rotation);
                projectile.transform.parent = playerProjectiles.transform;
                attack1NextAttack = Time.time + attackSpeedAttack1;
            }
        }

        if (Input.GetKey(KeyCode.Mouse1) && !laserAttackCooldown && !restrictAbilities)
        {
            laserFuel = Mathf.Clamp(laserFuel - (laserWasteTick * Time.deltaTime), laserMinFuel, laserMaxFuel);
            if (laserFuel != 0)
            {
                attack2Laser.SetActive(true);
            }
            else
            {
                laserAttackCooldown = true;
                attack2Laser.SetActive(false);
            }

        }
        else
        {
            attack2Laser.SetActive(false);
            if (laserFuel > laserMinFuelToAttackAfterCooldown)
            {
                laserAttackCooldown = false;
            }
        }

        if (Input.GetKey(KeyCode.Space) && !restrictAbilities)
        {
            if (playerCont.shield >= minShieldToAttack && !shieldAttackStart)
            {
                playerCont.shield -= minShieldToAttack;
                shield.SetActive(true);
                shieldAttackStart = true;
            }
        }
        else
        {
            if (shieldAttackStart)
            {
                explodeShield();
            }

        }
        if (shieldAttackStart)
        {
            playerCont.shield = Mathf.Clamp(playerCont.shield - (shieldWasteTick * Time.deltaTime), 0, 100);
            if (playerCont.shield == 0)
            {
                explodeShield();
            }
        }
    }
    void explodeShield()
    {
        //boom
        float multiplier = shield.GetComponent<PlayerShieldController>().powerMultiplier;

        GameObject projectile = Instantiate(playerShieldAttackPrefab, transform.position, shield.transform.rotation);
        projectile.transform.parent = playerProjectiles.transform;
        projectile.GetComponent<PlayerShieldAttackProjectileController>().powerMultiplier = multiplier;
        projectile.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        shield.GetComponent<PlayerShieldController>().powerMultiplier = 1;
        shield.SetActive(false);
        shieldAttackStart = false;
    }



}
