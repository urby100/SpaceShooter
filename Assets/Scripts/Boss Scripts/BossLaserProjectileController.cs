using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserProjectileController : MonoBehaviour
{
    public GameObject boss;
    public GameObject path;
    List<Transform> pathNodes = new List<Transform>();
    public GameObject laser;
    public int laserOffNodeNum = 4;
    public int laserOnNodeNum = 6;
    int nodeIterator = 0;
    float nodePathingAccuracy = 0.1f;
    public float movementSpeed = 15;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "BossLaserProjectile";
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
        laser.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        transform.rotation = boss.transform.rotation * Quaternion.Euler(0, 0, 90);
        GetComponent<BoxCollider2D>().enabled = true;
        laser.SetActive(true);
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            pathNodes[nodeIterator].position,
            Time.fixedDeltaTime * movementSpeed);
        float distance = Vector3.Distance(pathNodes[nodeIterator].position, transform.position);
        if (distance <= nodePathingAccuracy)
        {
            nodeIterator++;
            if (nodeIterator >= laserOnNodeNum +3)
            {
                Destroy(gameObject);
            }
        }
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
        transform.rotation = boss.transform.rotation * Quaternion.Euler(0, 0, 90);

    }
}
