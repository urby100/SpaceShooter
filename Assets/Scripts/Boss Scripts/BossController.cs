using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject enemyProjectiles;
    public GameObject enemies;
    public GameManager GM;
    public List<GameObject> bossStages;
    public int i = 0;
    public GameObject movePath;
    List<Transform> pathNodes = new List<Transform>();
    int nodeIterator = 0;
    float nodePathingAccuracy = 0.3f;
    float movementSpeed = 2f;
    public float movementSpeedMultiplier = 1;
    Vector3 pointDirection = Vector3.zero;
    float bossRotationSpeed = 10f;

    float health = 100;
    float takeDamageAmount = 1f;
    float damageTickDelay = 0.03f;
    float damageTickTime;
    float scoreMultiplier = 100;


    bool turnFasterAfterStageEnd = false;
    float turnFasterLasts = 3f;
    float turnFasterTime;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Boss";
        turnFasterAfterStageEnd = true;
        turnFasterTime=Time.time+turnFasterLasts;
        int counter = 0;
        float closestNodeDistance = Vector3.Distance(movePath.transform.GetChild(counter).position, transform.position);
        Vector3 closestNode = movePath.transform.GetChild(counter).position;
        foreach (Transform node in movePath.transform)
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
        turnFasterAfterStageEnd = true;
        turnFasterTime = Time.time + turnFasterLasts;
    }
    
    void FixedUpdate()
    {
        Debug.Log(health);
        if (turnFasterAfterStageEnd && Time.time > turnFasterTime) {
            bossStages[i].SetActive(true);
            turnFasterAfterStageEnd = false;
        }
        //rotation
        Vector2 vectorToTarget = pointDirection - transform.position;
        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90 - bossStages[i].transform.localEulerAngles.z;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation,  bossRotationSpeed * Time.deltaTime);
        
        //move on path
        transform.position = Vector3.MoveTowards(transform.position,
            pathNodes[nodeIterator].position,
            Time.fixedDeltaTime * movementSpeed* movementSpeedMultiplier);
        float distance = Vector3.Distance(pathNodes[nodeIterator].position, transform.position);
        if (distance <= nodePathingAccuracy)
        {
            nodeIterator++;
            if (nodeIterator >= pathNodes.Count)
            {
                nodeIterator = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerProjectiles"))
        {
            takeDamage();
        }
        if (collision.gameObject.name == "PlayerShield" || collision.gameObject.name == "PlayerShieldAttack")
        {
            takeDamage();
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
        }

    }
    void takeDamage()
    {
        if (turnFasterAfterStageEnd) { return; }
        health -= takeDamageAmount;
        if (health <= 0)
        {
            GM.AddScore(scoreMultiplier*(i+1));
            bossStages[i].SetActive(false);
            i++;
            if (i >= bossStages.Count)
            {
                clearEnemiesAndProjectiles();
                Destroy(gameObject);
            }
            else {
                turnFasterAfterStageEnd = true;
                movementSpeedMultiplier = 1;
                 health = 100;
                turnFasterTime = Time.time + turnFasterLasts;
                clearEnemiesAndProjectiles();
            }

        }
    }
    void clearEnemiesAndProjectiles() {
        foreach (Transform projectile in enemyProjectiles.transform) {
            Destroy(projectile.gameObject);
        }
        foreach (Transform enemy in enemies.transform)
        {
            Destroy(enemy.gameObject);
        }

    }
}
