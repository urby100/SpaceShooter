using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileController : MonoBehaviour
{
    public GameObject path;
    List<Transform> pathNodes = new List<Transform>();
    int nodeIterator = 0;
    float nodePathingAccuracy = 0.1f;
    public float movementSpeed = 15;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "BossProjectile";
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
        transform.position = Vector3.MoveTowards(transform.position,
            pathNodes[nodeIterator].position,
            Time.fixedDeltaTime * movementSpeed);
        float distance = Vector3.Distance(pathNodes[nodeIterator].position, transform.position);
        if (distance <= nodePathingAccuracy)
        {
            nodeIterator++;
            if (nodeIterator >= pathNodes.Count)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
