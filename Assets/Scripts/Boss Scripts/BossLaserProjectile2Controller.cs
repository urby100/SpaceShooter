using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserProjectile2Controller : MonoBehaviour
{
    bool forward = true;
    bool setLaser = true;
    public GameObject boss;
    public GameObject path1;
    public GameObject path2;
    List<Transform> pathNodes1 = new List<Transform>();
    List<Transform> pathNodes2 = new List<Transform>();
    public GameObject laser;
    public int laserOffNodeNum = 4;
    public int laserOnNodeNum = 6;
    int nodeIterator = 0;
    float nodePathingAccuracy = 0.1f;
    public float movementSpeed = 15;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "BossLaserProjectile2";
        int counter = 0;
        float closestNodeDistance = Vector3.Distance(path1.transform.GetChild(counter).position, transform.position);
        Vector3 closestNode = path1.transform.GetChild(counter).position;
        foreach (Transform node in path1.transform)
        {
            pathNodes1.Add(node);
            float distance = Vector3.Distance(node.position, transform.position);
            if (distance < closestNodeDistance)
            {
                closestNodeDistance = distance;
                closestNode = node.position;
                nodeIterator = counter;
            }
            counter++;
        }
        foreach (Transform node in path2.transform)
        {
            pathNodes2.Add(node);
        }
        laser.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        transform.rotation = boss.transform.rotation * Quaternion.Euler(0, 0, 90);
        GetComponent<BoxCollider2D>().enabled = true;
        laser.SetActive(true);
    }
    void FixedUpdate()
    {

        if (forward)
        {

            transform.position = Vector3.MoveTowards(transform.position,
                pathNodes1[nodeIterator].position,
                Time.fixedDeltaTime * movementSpeed);
            float distance = Vector3.Distance(pathNodes1[nodeIterator].position, transform.position);
            if (distance <= nodePathingAccuracy)
            {
                nodeIterator++;
                if (nodeIterator >= laserOnNodeNum )
                {
                    forward = false;
                    setLaser = false;
                    laser.SetActive(false);
                    GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            transform.rotation = boss.transform.rotation * Quaternion.Euler(0, 0, 90);
        }
        else
        {
            
            transform.position = Vector3.MoveTowards(transform.position,
              pathNodes2[nodeIterator].position,
              Time.fixedDeltaTime * movementSpeed);
            float distance = Vector3.Distance(pathNodes2[nodeIterator].position, transform.position);
            if (!setLaser && distance <= nodePathingAccuracy)
            {
                setLaser = true;
                
            }
            if (distance <= nodePathingAccuracy)
            {
                nodeIterator--;
                if (nodeIterator ==-1)
                {
                    Destroy(gameObject);
                }
            }
            transform.rotation = boss.transform.rotation * Quaternion.Euler(0, 0, -90);
        }

        if (setLaser)
        {
            if (nodeIterator >= laserOffNodeNum - 1 && nodeIterator <= laserOnNodeNum - 1)
            {
                laser.SetActive(false);
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                laser.SetActive(true);
                GetComponent<BoxCollider2D>().enabled = true;
            }
        }

    }
}
