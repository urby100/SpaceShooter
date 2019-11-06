using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerExample : MonoBehaviour
{
    public GameManager GM;
    public GameObject player;
    public GameObject enemy1_IQ1_Prefab;
    public GameObject enemy1_IQ2_Prefab;
    public GameObject enemy1_IQ3_Prefab;
    public GameObject spawnLines;

    float spawnEnemy1_IQ1_Delay=5f;
    float spawnEnemy1_IQ1_Time;
    int spawnEnemy1_IQ1_numberOfMaxSpawns = 3;

    float spawnEnemy1_IQ2_Delay = 8f;
    float spawnEnemy1_IQ2_Time;
    int spawnEnemy1_IQ2_numberOfMaxSpawns = 2;

    float spawnEnemy1_IQ3_Delay = 10f;
    float spawnEnemy1_IQ3_Time;
    int spawnEnemy1_IQ3_numberOfMaxSpawns = 1;

    void Start()
    {
        
        spawnEnemy1_IQ1_Time = Time.time + spawnEnemy1_IQ1_Delay;
        spawnEnemy1_IQ2_Time = Time.time + spawnEnemy1_IQ2_Delay;
        spawnEnemy1_IQ3_Time = Time.time + spawnEnemy1_IQ3_Delay;
    }

    void FixedUpdate()
    {
        if (Time.time > spawnEnemy1_IQ1_Time) {

            //select random spawn line
            GameObject spawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            for (int i = 0; i < spawnEnemy1_IQ1_numberOfMaxSpawns; i++) {
                Vector3 position = new Vector3(
                        Random.Range(spawnLine.transform.GetChild(0).position.x, spawnLine.transform.GetChild(1).position.x),
                        Random.Range(spawnLine.transform.GetChild(0).position.y, spawnLine.transform.GetChild(1).position.y),
                        0
                        );
                GameObject enemy = Instantiate(enemy1_IQ1_Prefab, position ,Quaternion.identity);
                enemy.transform.parent = gameObject.transform;
                enemy.GetComponent<Enemy1_IQ1Controller>().player = player;
                enemy.GetComponent<Enemy1_IQ1Controller>().GM = GM;
            }
            spawnEnemy1_IQ1_Time = Time.time + spawnEnemy1_IQ1_Delay;
        }
        
        if (Time.time > spawnEnemy1_IQ2_Time)
        {
            GameObject spawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            for (int i = 0; i < spawnEnemy1_IQ2_numberOfMaxSpawns; i++)
            {
                Vector3 position = new Vector3(
                          Random.Range(spawnLine.transform.GetChild(0).position.x, spawnLine.transform.GetChild(1).position.x),
                          Random.Range(spawnLine.transform.GetChild(0).position.y, spawnLine.transform.GetChild(1).position.y),
                          0
                          );
                GameObject enemy = Instantiate(enemy1_IQ2_Prefab, position, Quaternion.identity);
                enemy.transform.parent = gameObject.transform;
                enemy.GetComponent<Enemy1_IQ2Controller>().player = player;
                enemy.GetComponent<Enemy1_IQ2Controller>().GM = GM;
            }
            spawnEnemy1_IQ2_Time = Time.time + spawnEnemy1_IQ2_Delay;
        }

        if (Time.time > spawnEnemy1_IQ3_Time)
        {
            GameObject spawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;

            for (int i = 0; i < spawnEnemy1_IQ3_numberOfMaxSpawns; i++)
            {
                Vector3 position = new Vector3(
                          Random.Range(spawnLine.transform.GetChild(0).position.x, spawnLine.transform.GetChild(1).position.x),
                          Random.Range(spawnLine.transform.GetChild(0).position.y, spawnLine.transform.GetChild(1).position.y),
                          0
                          );
                GameObject enemy = Instantiate(enemy1_IQ3_Prefab, position, Quaternion.identity);
                enemy.transform.parent = gameObject.transform;
                enemy.GetComponent<Enemy1_IQ3Controller>().player = player;
                enemy.GetComponent<Enemy1_IQ3Controller>().GM = GM;
            }
            spawnEnemy1_IQ3_Time = Time.time + spawnEnemy1_IQ3_Delay;
        }
    }


}
