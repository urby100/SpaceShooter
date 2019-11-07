using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_IQ3Controller : MonoBehaviour
{
    float health = 100f;
    float damageTakeAmount = 20f;
    float damageTickDelay = 0.03f;
    float damageTickTime;

    float scoreMultiplier = 30f;

    public GameObject player;
    public GameManager GM;
    public GameObject enemyProjectiles;

    public GameObject path;
    List<Transform> pathNodes = new List<Transform>();
    int nodeIterator = 0;
    float nodePathingAccuracy = 0.3f;

    public GameObject projectilePrefab;
    public GameObject projectileSpawnPoint;
    float attackSpeed = 1f;
    float nextAttackTime;

    float movementSpeed = 5;
    float rotationSpeed = 4f;

    int numberOfProjectiles = 3;
    float ProjectileDegreeSpread = 10;


    // Start is called before the first frame update

    void Start()
    {
        gameObject.name = "Enemy2_IQ3";
        int counter = 0;
        float closestNodeDistance = Vector3.Distance(path.transform.GetChild(counter).position, transform.position);
        Vector3 closestNode = path.transform.GetChild(counter).position;
        foreach (Transform node in path.transform)
        {
            pathNodes.Add(node);
            float distance = Vector3.Distance(node.position, transform.position);
            if (distance < closestNodeDistance) {
                closestNodeDistance = distance;
                closestNode = node.position;
                nodeIterator = counter;
            }
            counter++;

        }
    }

    void FixedUpdate()
    {
        //move on path
        transform.position = Vector3.MoveTowards(transform.position,
            pathNodes[nodeIterator].position,
            Time.fixedDeltaTime * movementSpeed);
        float distance = Vector3.Distance(pathNodes[nodeIterator].position, transform.position);
        if (distance <= nodePathingAccuracy)
        {
            nodeIterator++;
            if (nodeIterator >= pathNodes.Count)
            {
                nodeIterator = 0;
            }
        }

        //attack
        if (Time.time > nextAttackTime)
        {
            float individualDegree = ProjectileDegreeSpread / numberOfProjectiles;
            float degreeCounter = -(ProjectileDegreeSpread / 2);
            for (int i = 0; i < numberOfProjectiles; i++)
            {

                GameObject projectile = Instantiate(projectilePrefab,
                    projectileSpawnPoint.transform.position,
                    transform.rotation * Quaternion.Euler(0, 0, degreeCounter));
                if (numberOfProjectiles % 2 == 0)
                {
                    if (degreeCounter + individualDegree == 0)
                    {
                        degreeCounter = individualDegree;
                    }
                    else
                    {
                        degreeCounter += individualDegree;
                    }
                }
                else
                {
                    degreeCounter += individualDegree;
                }
                projectile.name = "Enemy2_IQ3_Projectile";
                projectile.GetComponent<Enemy2_ProjectileController>().bulletSpeed = 15f;
                projectile.transform.parent = enemyProjectiles.transform;
                nextAttackTime = Time.time + attackSpeed;
            }
        }
        //rotate towards player slowly
        Vector2 vectorToTarget = player.transform.position - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectiles"))
        {
            takeDamage();
        }
        else
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.name == "PlayerShield" || collision.gameObject.name == "PlayerShieldAttack")
        {
            GetComponent<spawnPowerUp>().spawn(player);
            GM.AddScore(scoreMultiplier);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectiles"))
        {
            if (Time.time > damageTickTime)
            {
                takeDamage();
                damageTickTime = Time.time + damageTickDelay;
            }
            //Destroy(collision.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void takeDamage()
    {
        health -= damageTakeAmount;
        if (health <= 0)
        {
            GetComponent<spawnPowerUp>().spawn(player);
            GM.AddScore(scoreMultiplier);
            Destroy(gameObject);
        }
    }
}
