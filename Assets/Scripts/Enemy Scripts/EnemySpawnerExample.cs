using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerExample : MonoBehaviour
{

    public bool spawnEnemies1_IQ1 = true;
    public bool spawnEnemies1_IQ2 = true;
    public bool spawnEnemies1_IQ3 = true;
    public bool spawnEnemies2_IQ1 = true;
    public bool spawnEnemies2_IQ2 = true;
    public bool spawnEnemies2_IQ3 = true;
    public bool spawnEnemies3_IQ1 = true;
    public bool spawnEnemies3_IQ2 = true;
    public bool spawnEnemies3_IQ3 = true;

    public float spawnEnemy1InLoopDelay = 0.2f;
    public float spawnEnemy2InLoopDelay = 0.2f;
    public float spawnEnemy3InLoopDelay = 0.2f;

    public GameManager GM;
    public GameObject player;

    public GameObject enemyPaths;
    public GameObject enemyProjectiles;
    public GameObject enemies;
    public GameObject spawnLines;

    public GameObject enemy3_IQ1_Prefab;
    public GameObject enemy3_IQ2_Prefab;
    public GameObject enemy3_IQ3_Prefab;

    GameObject enemy3_IQ1_SpawnLine;
    GameObject enemy3_IQ2_SpawnLine;
    GameObject enemy3_IQ3_SpawnLine;

    public float spawnEnemy3_IQ1_Delay = 20f;
    float spawnEnemy3_IQ1_Time;
    public int spawnEnemy3_IQ1_numberOfMaxSpawns = 3;
    

    public float spawnEnemy3_IQ2_Delay = 25f;
    float spawnEnemy3_IQ2_Time;
    public int spawnEnemy3_IQ2_numberOfMaxSpawns = 2;

    public float spawnEnemy3_IQ3_Delay = 30f;
    float spawnEnemy3_IQ3_Time;
    public int spawnEnemy3_IQ3_numberOfMaxSpawns = 1;
    


    public GameObject enemy2_IQ1_Prefab;
    public GameObject enemy2_IQ2_Prefab;
    public GameObject enemy2_IQ3_Prefab;
    GameObject enemy2_IQ1_SpawnLine;
    GameObject enemy2_IQ2_SpawnLine;
    GameObject enemy2_IQ3_SpawnLine;


    public float spawnEnemy2_IQ1_Delay = 10f;
    float spawnEnemy2_IQ1_Time;
    public int spawnEnemy2_IQ1_numberOfMaxSpawns = 3;

    public float spawnEnemy2_IQ2_Delay = 12f;
    float spawnEnemy2_IQ2_Time;
    public int spawnEnemy2_IQ2_numberOfMaxSpawns = 2;

    public float spawnEnemy2_IQ3_Delay = 15f;
    float spawnEnemy2_IQ3_Time;
    public int spawnEnemy2_IQ3_numberOfMaxSpawns = 1;


    public GameObject enemy1_IQ1_Prefab;
    public GameObject enemy1_IQ2_Prefab;
    public GameObject enemy1_IQ3_Prefab;
    GameObject enemy1_IQ1_SpawnLine;
    GameObject enemy1_IQ2_SpawnLine;
    GameObject enemy1_IQ3_SpawnLine;

    public float spawnEnemy1_IQ1_Delay =5f;
    float spawnEnemy1_IQ1_Time;
    public int spawnEnemy1_IQ1_numberOfMaxSpawns = 3;

    public float spawnEnemy1_IQ2_Delay = 12f;
    float spawnEnemy1_IQ2_Time;
    public int spawnEnemy1_IQ2_numberOfMaxSpawns = 2;

    public float spawnEnemy1_IQ3_Delay = 15f;
    float spawnEnemy1_IQ3_Time;
    public int spawnEnemy1_IQ3_numberOfMaxSpawns = 1;

    void Start()
    {

        spawnEnemy3_IQ1_Time = Time.time + spawnEnemy3_IQ1_Delay;
        spawnEnemy3_IQ2_Time = Time.time + spawnEnemy3_IQ2_Delay;
        spawnEnemy3_IQ3_Time = Time.time + spawnEnemy3_IQ3_Delay;

        spawnEnemy2_IQ1_Time = Time.time + spawnEnemy2_IQ1_Delay;
        spawnEnemy2_IQ2_Time = Time.time + spawnEnemy2_IQ2_Delay;
        spawnEnemy2_IQ3_Time = Time.time + spawnEnemy2_IQ3_Delay;

        spawnEnemy1_IQ1_Time = Time.time + spawnEnemy1_IQ1_Delay;
        spawnEnemy1_IQ2_Time = Time.time + spawnEnemy1_IQ2_Delay;
        spawnEnemy1_IQ3_Time = Time.time + spawnEnemy1_IQ3_Delay;
    }
    void FixedUpdate()
    {
        //Enemy 3
        if (Time.time > spawnEnemy3_IQ1_Time && spawnEnemies3_IQ1)
        {
            //select random spawn line
            enemy3_IQ1_SpawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            float loopSpawnDelay = spawnEnemy3InLoopDelay;
            for (int i = 0; i < spawnEnemy3_IQ1_numberOfMaxSpawns; i++)
            {
                Invoke("spawnEnemy3_IQ1", loopSpawnDelay);
                loopSpawnDelay += spawnEnemy3InLoopDelay;
            }
            spawnEnemy3_IQ1_Time = Time.time + spawnEnemy3_IQ1_Delay;
        }

        if (Time.time > spawnEnemy3_IQ2_Time && spawnEnemies3_IQ2)
        {
            enemy3_IQ2_SpawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            float loopSpawnDelay = spawnEnemy3InLoopDelay;
            for (int i = 0; i < spawnEnemy3_IQ2_numberOfMaxSpawns; i++)
            {
                Invoke("spawnEnemy3_IQ2", loopSpawnDelay);
                loopSpawnDelay += spawnEnemy3InLoopDelay;
            }
            spawnEnemy3_IQ2_Time = Time.time + spawnEnemy3_IQ2_Delay;
        }

        if (Time.time > spawnEnemy3_IQ3_Time && spawnEnemies3_IQ3)
        {
            enemy3_IQ3_SpawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            float loopSpawnDelay = spawnEnemy3InLoopDelay;
            for (int i = 0; i < spawnEnemy3_IQ3_numberOfMaxSpawns; i++)
            {
                Invoke("spawnEnemy3_IQ3", loopSpawnDelay);
                loopSpawnDelay += spawnEnemy3InLoopDelay;
            }
            spawnEnemy3_IQ3_Time = Time.time + spawnEnemy3_IQ3_Delay;
        }


        //Enemy 2
        if (Time.time > spawnEnemy2_IQ1_Time && spawnEnemies2_IQ1)
        {

            //select random spawn line
            enemy2_IQ1_SpawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            float loopSpawnDelay = spawnEnemy2InLoopDelay;
            for (int i = 0; i < spawnEnemy2_IQ1_numberOfMaxSpawns; i++)
            {
                Invoke("spawnEnemy2_IQ1", loopSpawnDelay);

                loopSpawnDelay += spawnEnemy3InLoopDelay;

            }
            spawnEnemy2_IQ1_Time = Time.time + spawnEnemy2_IQ1_Delay;
        }
        
        if (Time.time > spawnEnemy2_IQ2_Time && spawnEnemies2_IQ2)
        {
            enemy2_IQ2_SpawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            float loopSpawnDelay = spawnEnemy2InLoopDelay;
            for (int i = 0; i < spawnEnemy2_IQ2_numberOfMaxSpawns; i++)
            {
                Invoke("spawnEnemy2_IQ2", loopSpawnDelay);

                loopSpawnDelay += spawnEnemy3InLoopDelay;
            }
            spawnEnemy2_IQ2_Time = Time.time + spawnEnemy2_IQ2_Delay;
        }
        
        if (Time.time > spawnEnemy2_IQ3_Time && spawnEnemies2_IQ3)
        {
            enemy2_IQ3_SpawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            float loopSpawnDelay = spawnEnemy2InLoopDelay;
            for (int i = 0; i < spawnEnemy2_IQ3_numberOfMaxSpawns; i++)
            {
                Invoke("spawnEnemy2_IQ3", loopSpawnDelay);

                loopSpawnDelay += spawnEnemy3InLoopDelay;
            }
            spawnEnemy2_IQ3_Time = Time.time + spawnEnemy2_IQ3_Delay;
        }
        //Enemy 1
        if (Time.time > spawnEnemy1_IQ1_Time && spawnEnemies1_IQ1) {

            //select random spawn line
            enemy1_IQ1_SpawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            float loopSpawnDelay = spawnEnemy2InLoopDelay;
            for (int i = 0; i < spawnEnemy1_IQ1_numberOfMaxSpawns; i++)
            {
                Invoke("spawnEnemy1_IQ1", loopSpawnDelay);
                loopSpawnDelay += spawnEnemy3InLoopDelay;
            }
            spawnEnemy1_IQ1_Time = Time.time + spawnEnemy1_IQ1_Delay;
        }
        
        if (Time.time > spawnEnemy1_IQ2_Time && spawnEnemies1_IQ2)
        {

            enemy1_IQ2_SpawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            float loopSpawnDelay = spawnEnemy2InLoopDelay;
            for (int i = 0; i < spawnEnemy1_IQ2_numberOfMaxSpawns; i++)
            {
                Invoke("spawnEnemy1_IQ2", loopSpawnDelay);
                loopSpawnDelay += spawnEnemy3InLoopDelay;
            }
            spawnEnemy1_IQ2_Time = Time.time + spawnEnemy1_IQ2_Delay;
        }

        if (Time.time > spawnEnemy1_IQ3_Time && spawnEnemies1_IQ3) 
        {
            enemy1_IQ3_SpawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            float loopSpawnDelay = spawnEnemy2InLoopDelay;
            for (int i = 0; i < spawnEnemy1_IQ3_numberOfMaxSpawns; i++)
            {
                Invoke("spawnEnemy1_IQ3", loopSpawnDelay);
                loopSpawnDelay += spawnEnemy3InLoopDelay;
            }
            spawnEnemy1_IQ3_Time = Time.time + spawnEnemy1_IQ3_Delay;
        }
    }

    public void spawnEnemy3_IQ1() {

        Vector3 position = new Vector3(
                Random.Range(enemy3_IQ1_SpawnLine.transform.GetChild(0).position.x, enemy3_IQ1_SpawnLine.transform.GetChild(1).position.x),
                Random.Range(enemy3_IQ1_SpawnLine.transform.GetChild(0).position.y, enemy3_IQ1_SpawnLine.transform.GetChild(1).position.y),
                0
                );
        GameObject enemy = Instantiate(enemy3_IQ1_Prefab, position, Quaternion.identity);
        enemy.transform.parent = enemies.transform;
        enemy.GetComponent<Enemy3_Controller>().player = player;
        enemy.GetComponent<Enemy3_Controller>().GM = GM;
        enemy.GetComponent<Enemy3_Controller>().enemyProjectiles = enemyProjectiles;
        enemy.GetComponent<Enemy3_Controller>().path =
            enemyPaths.transform.GetChild(Random.Range(0, enemyPaths.transform.childCount)).gameObject;
    }
    public void spawnEnemy3_IQ2()
    {

        Vector3 position = new Vector3(
                  Random.Range(enemy3_IQ2_SpawnLine.transform.GetChild(0).position.x, enemy3_IQ2_SpawnLine.transform.GetChild(1).position.x),
                  Random.Range(enemy3_IQ2_SpawnLine.transform.GetChild(0).position.y, enemy3_IQ2_SpawnLine.transform.GetChild(1).position.y),
                  0
                  );
        GameObject enemy = Instantiate(enemy3_IQ2_Prefab, position, Quaternion.identity);
        enemy.transform.parent = enemies.transform;
        enemy.GetComponent<Enemy3_Controller>().player = player;
        enemy.GetComponent<Enemy3_Controller>().GM = GM;
        enemy.GetComponent<Enemy3_Controller>().enemyProjectiles = enemyProjectiles;
        enemy.GetComponent<Enemy3_Controller>().path =
            enemyPaths.transform.GetChild(Random.Range(0, enemyPaths.transform.childCount)).gameObject;
    }
    public void spawnEnemy3_IQ3()
    {
        Vector3 position = new Vector3(
                  Random.Range(enemy3_IQ3_SpawnLine.transform.GetChild(0).position.x, enemy3_IQ3_SpawnLine.transform.GetChild(1).position.x),
                  Random.Range(enemy3_IQ3_SpawnLine.transform.GetChild(0).position.y, enemy3_IQ3_SpawnLine.transform.GetChild(1).position.y),
                  0
                  );
        GameObject enemy = Instantiate(enemy3_IQ3_Prefab, position, Quaternion.identity);
        enemy.transform.parent = enemies.transform;
        enemy.GetComponent<Enemy3_Controller>().player = player;
        enemy.GetComponent<Enemy3_Controller>().GM = GM;
        enemy.GetComponent<Enemy3_Controller>().enemyProjectiles = enemyProjectiles;
        enemy.GetComponent<Enemy3_Controller>().path =
            enemyPaths.transform.GetChild(Random.Range(0, enemyPaths.transform.childCount)).gameObject;

    }

    public void spawnEnemy2_IQ1()
    {
        Vector3 position = new Vector3(
                Random.Range(enemy2_IQ1_SpawnLine.transform.GetChild(0).position.x, enemy2_IQ1_SpawnLine.transform.GetChild(1).position.x),
                Random.Range(enemy2_IQ1_SpawnLine.transform.GetChild(0).position.y, enemy2_IQ1_SpawnLine.transform.GetChild(1).position.y),
                0
                );
        GameObject enemy = Instantiate(enemy2_IQ1_Prefab, position, Quaternion.identity);
        enemy.transform.parent = enemies.transform;
        enemy.GetComponent<Enemy2_IQ1Controller>().player = player;
        enemy.GetComponent<Enemy2_IQ1Controller>().GM = GM;
        enemy.GetComponent<Enemy2_IQ1Controller>().enemyProjectiles = enemyProjectiles;
        enemy.GetComponent<Enemy2_IQ1Controller>().path =
            enemyPaths.transform.GetChild(Random.Range(0, enemyPaths.transform.childCount)).gameObject;
    }
    public void spawnEnemy2_IQ2()
    {
        Vector3 position = new Vector3(
                  Random.Range(enemy2_IQ2_SpawnLine.transform.GetChild(0).position.x, enemy2_IQ2_SpawnLine.transform.GetChild(1).position.x),
                  Random.Range(enemy2_IQ2_SpawnLine.transform.GetChild(0).position.y, enemy2_IQ2_SpawnLine.transform.GetChild(1).position.y),
                  0
                  );
        GameObject enemy = Instantiate(enemy2_IQ2_Prefab, position, Quaternion.identity);
        enemy.transform.parent = enemies.transform;
        enemy.GetComponent<Enemy2_IQ2Controller>().player = player;
        enemy.GetComponent<Enemy2_IQ2Controller>().GM = GM;
        enemy.GetComponent<Enemy2_IQ2Controller>().enemyProjectiles = enemyProjectiles;
        enemy.GetComponent<Enemy2_IQ2Controller>().path =
            enemyPaths.transform.GetChild(Random.Range(0, enemyPaths.transform.childCount)).gameObject;
    }
    public void spawnEnemy2_IQ3()
    {
        Vector3 position = new Vector3(
                  Random.Range(enemy2_IQ3_SpawnLine.transform.GetChild(0).position.x, enemy2_IQ3_SpawnLine.transform.GetChild(1).position.x),
                  Random.Range(enemy2_IQ3_SpawnLine.transform.GetChild(0).position.y, enemy2_IQ3_SpawnLine.transform.GetChild(1).position.y),
                  0
                  );
        GameObject enemy = Instantiate(enemy2_IQ3_Prefab, position, Quaternion.identity);
        enemy.transform.parent = enemies.transform;
        enemy.GetComponent<Enemy2_IQ3Controller>().player = player;
        enemy.GetComponent<Enemy2_IQ3Controller>().GM = GM;
        enemy.GetComponent<Enemy2_IQ3Controller>().enemyProjectiles = enemyProjectiles;
        enemy.GetComponent<Enemy2_IQ3Controller>().path =
            enemyPaths.transform.GetChild(Random.Range(0, enemyPaths.transform.childCount)).gameObject;
    }
    public void spawnEnemy1_IQ1() {


        Vector3 position = new Vector3(
                Random.Range(enemy1_IQ1_SpawnLine.transform.GetChild(0).position.x, enemy1_IQ1_SpawnLine.transform.GetChild(1).position.x),
                Random.Range(enemy1_IQ1_SpawnLine.transform.GetChild(0).position.y, enemy1_IQ1_SpawnLine.transform.GetChild(1).position.y),
                0
                );
        GameObject enemy = Instantiate(enemy1_IQ1_Prefab, position, Quaternion.identity);
        enemy.transform.parent = enemies.transform;
        enemy.GetComponent<Enemy1_IQ1Controller>().player = player;
        enemy.GetComponent<Enemy1_IQ1Controller>().GM = GM;
    }
    public void spawnEnemy1_IQ2()
    {
        Vector3 position = new Vector3(
                  Random.Range(enemy1_IQ2_SpawnLine.transform.GetChild(0).position.x, enemy1_IQ2_SpawnLine.transform.GetChild(1).position.x),
                  Random.Range(enemy1_IQ2_SpawnLine.transform.GetChild(0).position.y, enemy1_IQ2_SpawnLine.transform.GetChild(1).position.y),
                  0
                  );
        GameObject enemy = Instantiate(enemy1_IQ2_Prefab, position, Quaternion.identity);
        enemy.transform.parent = enemies.transform;
        enemy.GetComponent<Enemy1_IQ2Controller>().player = player;
        enemy.GetComponent<Enemy1_IQ2Controller>().GM = GM;

    }
    public void spawnEnemy1_IQ3()
    {
        Vector3 position = new Vector3(
                  Random.Range(enemy1_IQ3_SpawnLine.transform.GetChild(0).position.x, enemy1_IQ3_SpawnLine.transform.GetChild(1).position.x),
                  Random.Range(enemy1_IQ3_SpawnLine.transform.GetChild(0).position.y, enemy1_IQ3_SpawnLine.transform.GetChild(1).position.y),
                  0
                  );
        GameObject enemy = Instantiate(enemy1_IQ3_Prefab, position, Quaternion.identity);
        enemy.transform.parent = enemies.transform;
        enemy.GetComponent<Enemy1_IQ3Controller>().player = player;
        enemy.GetComponent<Enemy1_IQ3Controller>().GM = GM;

    }
}
