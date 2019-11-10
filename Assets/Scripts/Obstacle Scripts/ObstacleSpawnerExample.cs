using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerExample : MonoBehaviour
{
    public bool spawnObstacles1 = true;
    public bool spawnObstacles2 = true;

    public GameObject warningSignPrefab;
    public GameObject obstacle1Prefab;
    public GameObject obstacle2Prefab;
    public GameObject spawnLines;
    public Transform obstacles;

    float warnXSecondsBeforeSpawn = 2f;
    float warnObstacle2SpawnTime;

    public float spawnObstacle1_Delay = 10f;
    float spawnObstacle1_Time;
    public int spawnObstacle1_numberOfMinSpawns = 4;
    public int spawnObstacle1_numberOfMaxSpawns = 8;

    public float spawnObstacle2_Delay = 18f;
    float spawnObstacle2_Time;
    public int spawnObstacle2_numberOfMinSpawns = 2;
    public int spawnObstacle2_numberOfMaxSpawns = 4;

    public float spawnOffsetFromCenter_axisX = 5f;
    public float spawnOffsetFromCenter_axisY = 2.5f;

    List<Vector3> obstacle1SpawnPositions;
    List<Vector2> obstacle1DirectionList;
    List<Vector3> obstacle2SpawnPositions;
    List<Vector2> obstacle2DirectionList;

    bool warnedObstacle2Spawn = false;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnObstacles1)
        {
            setSpawnInfoObstacle1();
        }
        if (spawnObstacles2)
        {
            setSpawnInfoObstacle2();
        }
    }

    void FixedUpdate()
    {

        if (Time.time > spawnObstacle1_Time)
        {
            if (spawnObstacles1)
            {
                spawnObstacle1();
                setSpawnInfoObstacle1();
            }
        }
        if (Time.time > spawnObstacle2_Time)
        {
            if (spawnObstacles2)
            {
                spawnObstacle2();
                setSpawnInfoObstacle2();
            }
        }
        if (Time.time > warnObstacle2SpawnTime && !warnedObstacle2Spawn) {
            if (spawnObstacles2)
            {
                SpawnWarningSignForObstacle2();
            }
        }
        
    }
    void SpawnWarningSignForObstacle2()
    {
        warnedObstacle2Spawn = true;
        for (int i = 0; i < obstacle2SpawnPositions.Count; i++)
        {
            GameObject warningSign = Instantiate(warningSignPrefab,obstacle2SpawnPositions[i], Quaternion.identity);
            warningSign.transform.parent = obstacles;
            warningSign.GetComponent<WarningSignController>().destroyDelay=warnXSecondsBeforeSpawn;

            float angle = (Mathf.Atan2(obstacle2DirectionList[i].y, obstacle2DirectionList[i].x) * Mathf.Rad2Deg) - 90;
            warningSign.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            warningSign.transform.position = warningSign.transform.position + 5*warningSign.transform.up;
        }
    }
    void setSpawnInfoObstacle1()
    {
        obstacle1SpawnPositions = new List<Vector3>();
        obstacle1DirectionList = new List<Vector2>();
        //select random spawn line
        GameObject spawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
        int numberOfspawns = Random.Range(spawnObstacle1_numberOfMinSpawns, spawnObstacle1_numberOfMaxSpawns + 1);
        for (int i = 0; i < numberOfspawns; i++)
        {
            Vector3 position = new Vector3(
                    Random.Range(spawnLine.transform.GetChild(0).position.x, spawnLine.transform.GetChild(1).position.x),
                    Random.Range(spawnLine.transform.GetChild(0).position.y, spawnLine.transform.GetChild(1).position.y),
                    0
                    );
            obstacle1SpawnPositions.Add(position);

            float offset_axisX = Random.Range(-spawnOffsetFromCenter_axisX, spawnOffsetFromCenter_axisX);
            float offset_axisY = Random.Range(-spawnOffsetFromCenter_axisY, spawnOffsetFromCenter_axisY);
            Vector2 heading = new Vector3(offset_axisX, offset_axisY, 0) - position;
            float distance = heading.magnitude;
            Vector2 direction = heading / distance; // This is now the normalized direction.

            obstacle1DirectionList.Add(direction);
        }
        spawnObstacle1_Time = Time.time + spawnObstacle1_Delay;
    }
    void spawnObstacle1()
    {
        for(int i=0; i < obstacle1SpawnPositions.Count;i++)
        {
            GameObject obstacle = Instantiate(obstacle1Prefab, obstacle1SpawnPositions[i], Quaternion.identity);
            obstacle.transform.parent = obstacles;
            obstacle.GetComponent<Obstacle1Controller>().moveVector = obstacle1DirectionList[i];
        }
    }
    void setSpawnInfoObstacle2()
    {
        obstacle2SpawnPositions = new List<Vector3>();
        obstacle2DirectionList = new List<Vector2>();
        //select random spawn line
        GameObject spawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
        int numberOfspawns = Random.Range(spawnObstacle2_numberOfMinSpawns, spawnObstacle2_numberOfMaxSpawns + 1);
        for (int i = 0; i < numberOfspawns; i++)
        {
            Vector3 position = new Vector3(
                    Random.Range(spawnLine.transform.GetChild(0).position.x, spawnLine.transform.GetChild(1).position.x),
                    Random.Range(spawnLine.transform.GetChild(0).position.y, spawnLine.transform.GetChild(1).position.y),
                    0
                    );
            obstacle2SpawnPositions.Add(position);

            float offset_axisX = Random.Range(-spawnOffsetFromCenter_axisX, spawnOffsetFromCenter_axisX);
            float offset_axisY = Random.Range(-spawnOffsetFromCenter_axisY, spawnOffsetFromCenter_axisY);
            Vector2 heading = new Vector3(offset_axisX, offset_axisY, 0) - position;
            float distance = heading.magnitude;
            Vector2 direction = heading / distance; // This is now the normalized direction.

            obstacle2DirectionList.Add(direction);
        }
        spawnObstacle2_Time = Time.time + spawnObstacle2_Delay;
        warnObstacle2SpawnTime = spawnObstacle2_Time - warnXSecondsBeforeSpawn;
        warnedObstacle2Spawn = false;
    }
    void spawnObstacle2()
    {
        for (int i = 0; i < obstacle2SpawnPositions.Count; i++)
        {
            GameObject obstacle = Instantiate(obstacle2Prefab, obstacle2SpawnPositions[i], Quaternion.identity);
            obstacle.transform.parent = obstacles;
            obstacle.GetComponent<Obstacle2Controller>().moveVector = obstacle2DirectionList[i];
        }
    }
}
