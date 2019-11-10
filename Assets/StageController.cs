using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HighScore
{
    public string name;
    public int score;

    public HighScore(string n, int s)
    {
        name = n;
        score = s;
    }

}
public class StageController : MonoBehaviour
{

    public GameManager GM;
    public PauseMenu PM;
    List<HighScore> highScores = new List<HighScore>();

    PlayerController playerControllerScript;
    PlayerAbilities playerAbilitiesScript;

    public float stageLasts = 15f;
    public float stageLastsMultiplier = 2f;
    float stageTime;

    int iterator;
    public List<GameObject> stages;
    public GameObject BossStage;
    bool newStage = false;
    public float newStageDelay = 2f;
    float newStageTime;

    bool bossOn = false;

    bool setWaitTimeWon = false;
    float waitTimeWon;
    bool saved = false;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GM.player.GetComponent<PlayerController>();
        playerAbilitiesScript = GM.player.GetComponent<PlayerAbilities>();
        highScores = new List<HighScore>();
        iterator = 0;
        stageTime = Time.time + stageLasts;
        stages[iterator].SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (!bossOn)
        {
            if (Time.time > stageTime && !newStage)
            {
                stages[iterator].SetActive(false);
                iterator++;
                if (iterator >= stages.Count)
                {
                    bossOn = true;
                }
                Debug.Log("New stage " + iterator);
                newStageTime = Time.time + newStageDelay;
                newStage = true;
            }
        }
        if (newStage && Time.time > newStageTime)
        {
            newStage = false;
            if (bossOn)
            {
                BossStage.SetActive(true);
            }
            else {

                stages[iterator].SetActive(true);
                stageTime = Time.time + (stageLasts * stageLastsMultiplier);
            }
        }
        if (bossOn && BossStage == null && !setWaitTimeWon)
        {
            setWaitTimeWon = true;
            waitTimeWon = Time.time + newStageDelay;
        }
        if (Time.time > waitTimeWon && setWaitTimeWon)
        {

            saveHighScore();
            PM.playerWon();
        }
        if (playerControllerScript.health <= 0 && !PM.isPaused)
        {
            saveHighScore();
                PM.playerDied();

        }

    }
    public void saveHighScore() {
        if (saved) {
            return;
        }
        saved = true;
        iterator = 0;
        highScores = new List<HighScore>();
        for (int i = 1; i < 11; i++)
        {
            highScores.Add(
                new HighScore(PlayerPrefs.GetString("TopName" + i.ToString(), "   "),
                PlayerPrefs.GetInt("TopScore" + i.ToString(), 0)));

        }
        highScores.Add(
               new HighScore(
                   PlayerPrefs.GetString("Name"),
               PlayerPrefs.GetInt("CurrentScore")
               )
               );
        highScores = highScores.OrderByDescending(HighScore => HighScore.score).ToList<HighScore>();
        highScores.Remove(highScores[highScores.Count - 1]);
        int j = 1;
        foreach (HighScore hs in highScores)
        {
            PlayerPrefs.SetString("TopName" + j.ToString(), hs.name);
            PlayerPrefs.SetInt("TopScore" + j.ToString(), hs.score);
            j++;
        }
    }
}
