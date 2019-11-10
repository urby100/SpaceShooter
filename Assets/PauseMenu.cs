using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject instructionsPanel;
    public GameObject highscorePanel;
    public Text helpText;
    public Text nameText;
    public Text highscoreText;
    public InputField nameInputField;
    public Button nameSaveButton;
    public GameObject resumeButton;
    public GameObject restartButton;
    public TextMeshProUGUI pauseMenuText;

    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("Name", "AAA") == "AAA")
        {
            PlayerPrefs.SetString("Name", "AAA");
        }
        nameInputField.text = PlayerPrefs.GetString("Name", "AAA");
        setHighscoreText();

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            isPaused = true;
        }
        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactiveMenu();
        }
    }
    public void playerDied()
    {
        pauseMenuText.text = "YOU DIED";
        resumeButton.SetActive(false);
        restartButton.SetActive(true);
        setHighscoreText();
        isPaused = true;
    }
    public void playerWon()
    {
        pauseMenuText.text = "YOU WON";
        resumeButton.SetActive(false);
        restartButton.SetActive(true);
        setHighscoreText();
        isPaused = true;
    }
    public void restartGame()
    {
        pauseMenuText.text = "Paused";
        DeactiveMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void ActivateMenu()
    {
        setHighscoreText();
        Time.timeScale = 0;
        helpText.enabled = false;
        PauseMenuUI.SetActive(true);
    }
    public void DeactiveMenu()
    {
        Time.timeScale = 1;
        helpText.enabled = true;
        PauseMenuUI.SetActive(false);
        isPaused = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void setHighscoreText() {
        highscoreText.text = "";
        for (int i = 1; i < 11; i++)
        {

            highscoreText.text += i.ToString().PadLeft(3, ' ') + (PlayerPrefs.GetString("TopName" + i.ToString(), "AAA")).PadLeft(5, ' ')
                + "  -  " + PlayerPrefs.GetInt("TopScore" + i.ToString(), 0).ToString().PadLeft(10, '0') + "\n";
        }
    }
    public void showHighscores()
    {
        nameInputField.text = PlayerPrefs.GetString("Name", "AAA");
        setHighscoreText();
        instructionsPanel.SetActive(false);
        highscorePanel.SetActive(true);
    }
    public void showInstructions()
    {
        instructionsPanel.SetActive(true);
        highscorePanel.SetActive(false);
    }
    public void setName()
    {
        PlayerPrefs.SetString("Name", nameText.text);
    }
}
