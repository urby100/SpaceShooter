using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image healthBar;
    public Image shieldBar;
    public Text scoreText;

    public GameObject player;
    PlayerController playerControllerScript;
    PlayerAbilities playerAbilitiesScript;

    
    float score;
    float scoreUnit = 10;


    float addScoreEveryXSeconds = 0.1f;
    float addScoreTime;

    float beingAliveMultiplier = 1;

    public float noDmgTakenMultiplier;
    float noDmgTakenMultiplierMin = 1;
    float noDmgTakenMultiplierMax = 10;
    float noDmgTakenForXSeconds = 4f;
    float noDmgTakenTime;


    float previousHealthPoints;
    float previousShieldPoints;



    // Start is called before the first frame update
    void Start()
    {
        noDmgTakenMultiplier = 1;
        noDmgTakenTime = Time.time + noDmgTakenForXSeconds;
        addScoreTime = Time.time + addScoreEveryXSeconds;
        playerControllerScript = player.GetComponent<PlayerController>();
        playerAbilitiesScript = player.GetComponent<PlayerAbilities>();
        previousHealthPoints = playerControllerScript.health;
        previousShieldPoints = playerControllerScript.shield;
        updateHealthBar();
        updateShieldBar();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > noDmgTakenTime)//če v naslednjih noDmgTakenForXSeconds sekundah ne dobi player dmga se score multiplier poveča za 1
        {
            setNoDmgMultiplier(1);
            noDmgTakenTime = Time.time + noDmgTakenForXSeconds;
        }
        if (Time.time > addScoreTime)
        {
            AddScore(beingAliveMultiplier);
            addScoreTime = Time.time + addScoreEveryXSeconds;
        }
        if ((playerControllerScript.health != previousHealthPoints &&
            playerControllerScript.health > previousHealthPoints) ||
            (playerControllerScript.shield != previousShieldPoints &&
            playerControllerScript.shield > previousShieldPoints))
        {
            previousHealthPoints = playerControllerScript.health;
            previousShieldPoints = playerControllerScript.shield;
        }

        if ((playerControllerScript.health != previousHealthPoints &&
            playerControllerScript.health < previousHealthPoints) ||
            (playerControllerScript.shield != previousShieldPoints &&
            playerControllerScript.shield < previousShieldPoints))
        {
            noDmgTakenMultiplier = 1;
            noDmgTakenTime = Time.time + noDmgTakenForXSeconds;
            previousHealthPoints = playerControllerScript.health;
            previousShieldPoints = playerControllerScript.shield;
        }
        //Debug.Log(score + " " + noDmgTakenMultiplier);
    }
    public void AddScore(float multiplier)
    {
        score = score + (scoreUnit * multiplier * noDmgTakenMultiplier);
        scoreText.text = "(x"+noDmgTakenMultiplier.ToString()+") "+score.ToString().PadLeft(10, '0');
    }
    public void setNoDmgMultiplier(float addToScoreMultiplier)
    {
        noDmgTakenMultiplier = Mathf.Clamp(noDmgTakenMultiplier + addToScoreMultiplier, noDmgTakenMultiplierMin, noDmgTakenMultiplierMax);
    }
    public void updateHealthBar()
    {
        healthBar.fillAmount = playerControllerScript.health / 100;
    }
    public void updateShieldBar()
    {
        shieldBar.fillAmount = playerControllerScript.shield / 100;
    }
}
