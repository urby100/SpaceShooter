using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2_IQ1Controller : MonoBehaviour
{
    float health = 100f;
    float damageTakeAmount = 20f;
    float damageTickDelay = 0.03f;
    float damageTickTime;

    float scoreMultiplier = 10f;

    public GameObject player;
    public GameManager GM;
    public GameObject enemyProjectiles;

    public GameObject path;
    List<Transform> pathNodes = new List<Transform>();
    int nodeIterator = 0;
    Vector3 lastPosition;
    float nodePathingAccuracy = 0.3f;

    public GameObject projectilePrefab;
    public GameObject projectileSpawnPoint;
    float attackSpeed = 0.8f;
    float nextAttackTime;

    float movementSpeed = 3;
    float rotationSpeed = 1.5f;


    // Start is called before the first frame update

    void Start()
    {
        gameObject.name = "Enemy2_IQ1";
        int counter = 0;
        float closestNodeDistance = Vector3.Distance(path.transform.GetChild(counter).position, transform.position);
        Vector3 closestNode = path.transform.GetChild(counter).position;
        foreach (Transform node in path.transform)
        {
            pathNodes.Add(node);
            float distance = Vector3.Distance(node.position, transform.position);
            if (distance < closestNodeDistance)
            {
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
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, transform.rotation);
            projectile.name = "Enemy2_IQ1_Projectile";
            projectile.GetComponent<Enemy2_ProjectileController>().bulletSpeed = 10f;
            projectile.transform.parent = enemyProjectiles.transform;
            nextAttackTime = Time.time + attackSpeed;
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
