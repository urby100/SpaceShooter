using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image healthBar;
    public Image invincibleBar;
    public Image invincibleBar2;
    public Image nitroFuelBar;
    public Image laserFuelBar;
    public Image shieldBar;
    public Text scoreText;

    public GameObject player;
    PlayerController playerControllerScript;
    PlayerAbilities playerAbilitiesScript;

    
    float score;
    float scoreUnit = 10;
    public bool stopScore = false;


    float addScoreEveryXSeconds = 0.1f;
    float addScoreTime;

    float beingAliveMultiplier = 1;

    public float noDmgTakenMultiplier;
    float noDmgTakenMultiplierMin = 1;
    float noDmgTakenMultiplierMax = 10;
    float noDmgTakenForXSeconds = 4f;
    float noDmgTakenTime;
    



    // Start is called before the first frame update
    void Start()
    {
        noDmgTakenMultiplier = 1;
        noDmgTakenTime = Time.time + noDmgTakenForXSeconds;
        addScoreTime = Time.time + addScoreEveryXSeconds;
        playerControllerScript = player.GetComponent<PlayerController>();
        playerAbilitiesScript = player.GetComponent<PlayerAbilities>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerControllerScript.health > 0)
        {
            PlayerPrefs.SetInt("CurrentScore", (int)score);
        }
        if (playerControllerScript.invincible)
        {
            invincibleBar.fillAmount = 1;
            invincibleBar2.fillAmount = 1;
        }
        else
        {
            invincibleBar.fillAmount = 0;
            invincibleBar2.fillAmount = 0;
        }
        nitroFuelBar.fillAmount = playerAbilitiesScript.nitroFuel / 100;
        laserFuelBar.fillAmount = playerAbilitiesScript.laserFuel / 100;
        shieldBar.fillAmount = playerControllerScript.shield / 100;
        healthBar.fillAmount = playerControllerScript.health / 100;
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
        //Debug.Log(score + " " + noDmgTakenMultiplier);
    }
    public void AddScore(float multiplier)
    {
        if (playerControllerScript.health > 0)
        {
            if (!stopScore)
            {
                score = score + (scoreUnit * multiplier * noDmgTakenMultiplier);
            }
            scoreText.text = score.ToString().PadLeft(10, '0') + " (x" + noDmgTakenMultiplier.ToString() + ")";
        }
    }
    public void setNoDmgMultiplier(float addToScoreMultiplier)
    {
        noDmgTakenMultiplier = Mathf.Clamp(noDmgTakenMultiplier + addToScoreMultiplier, noDmgTakenMultiplierMin, noDmgTakenMultiplierMax);
    }
    public void resetScoreMultiplier()
    {
        noDmgTakenMultiplier = 1;
        noDmgTakenTime = Time.time + noDmgTakenForXSeconds;
    }
}
