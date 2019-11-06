using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerExample : MonoBehaviour
{
    public GameObject obstacle1Prefab;
    public GameObject spawnLines;

    float spawnObstacle1_Delay = 10f;
    float spawnObstacle1_Time;
    int sspawnObstacle1_numberOfMaxSpawns = 7;

    float spawnOffsetFromCenter_axisX = 5f;
    float spawnOffsetFromCenter_axisY = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        spawnObstacle1_Time = Time.time + spawnObstacle1_Delay;
    }

    void FixedUpdate()
    {
        if (Time.time > spawnObstacle1_Time)
        {

            //select random spawn line
            GameObject spawnLine = spawnLines.transform.GetChild(Random.Range(0, spawnLines.transform.childCount)).gameObject;
            float offset_axisX = Random.Range(-spawnOffsetFromCenter_axisX, spawnOffsetFromCenter_axisX);
            float offset_axisY = Random.Range(-spawnOffsetFromCenter_axisY, spawnOffsetFromCenter_axisY);
            for (int i = 0; i < sspawnObstacle1_numberOfMaxSpawns; i++)
            {
                Vector3 position = new Vector3(
                        Random.Range(spawnLine.transform.GetChild(0).position.x, spawnLine.transform.GetChild(1).position.x),
                        Random.Range(spawnLine.transform.GetChild(0).position.y, spawnLine.transform.GetChild(1).position.y),
                        0
                        );
                GameObject obstacle = Instantiate(obstacle1Prefab, position, Quaternion.identity);
                obstacle.transform.parent = gameObject.transform;

                

                Vector2 heading = new Vector3(offset_axisX, offset_axisY, 0) - obstacle.transform.position;
                float distance = heading.magnitude;
                Vector2 direction = heading / distance; // This is now the normalized direction.
                obstacle.GetComponent<Obstacle1Controller>().moveVector = direction;
            }
            spawnObstacle1_Time = Time.time + spawnObstacle1_Delay;
        }
    }
}
