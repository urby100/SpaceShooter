using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPowerUp : MonoBehaviour
{
    public GameObject powerUpPrefab;
    float randomValue;
    float spawnChance = 0.8f;// 20 %
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawn(GameObject player) {
        randomValue = Random.value;//random med 0 in 1
        if (Random.value > spawnChance) 
        {
            GameObject powerUp = Instantiate(powerUpPrefab, transform.position, transform.rotation);
            powerUp.GetComponent<PowerUpController>().player = player;
        }

    }
}
