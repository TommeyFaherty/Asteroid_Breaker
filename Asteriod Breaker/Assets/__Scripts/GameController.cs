using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // == fields ==
    private int playerScore = 0;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject highScorePanel;
    public GameObject inGameUI;
    private bool paused = false;
    public TextMeshProUGUI score;
    public Text playerName;
    public InputField input;
    public AudioSource song1;
    public AudioSource song2;

    void Start()
    {
        playerName.text = PlayerPrefs.GetString("player_name");
        Debug.Log("Player name: "+playerName.text);
    }

    void Update()
    {

        //Check if the ecape key was pressed to pause
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }

        }

        //update player score saved
        score.text = playerScore.ToString();
        PlayerPrefs.SetInt("player_score", playerScore);
    }
    // == event handling ==
    // subscribe in the OnEnable method
    private void OnEnable()
    {
        Enemy.EnemyKilledEvent += HandleEnemyKilledEvent;
    }

    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= HandleEnemyKilledEvent;
    }

    public void HandleEnemyKilledEvent(Enemy enemy)
    {
        // add the score from the enemy to the player
        playerScore += enemy.ScoreValue;
        Debug.Log("Score: " + playerScore);
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        paused = true;
        
        song1.volume = song1.volume / 2;

        //Use listener to get name of player
        input.onEndEdit.AddListener(delegate {
            SubmitName(input);
        });
    }

    private void SubmitName(InputField userInput)
    {
        Debug.Log("Name: " + userInput.text);
        PlayerPrefs.SetString("player_name", userInput.text);
    }

    private void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        paused = false;
        song1.volume = song1.volume * 2;
    }

    private void Quit()
    {
        //Time Scale is reverted back to normal here as it is a global value
        //and would remain 0 otherwise
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER!!!");
        PlayerPrefs.SetInt("player_score", playerScore);

        Time.timeScale = 0;

        gameOverPanel.SetActive(true);
        inGameUI.SetActive(false);

        highScorePanel.SetActive(true);

        //Get final scoreBoard
        GameObject highScoreTable = GameObject.Find("HighScoreTable");
        ScoreBoard scoreBoard = (ScoreBoard)highScoreTable.GetComponent(typeof(ScoreBoard));
        scoreBoard.GetScoreBoard();
        highScorePanel.SetActive(false);

        //Change music
        song2.volume = song1.volume;
        song1.Stop();
        song2.PlayDelayed(6f);
    }
}
