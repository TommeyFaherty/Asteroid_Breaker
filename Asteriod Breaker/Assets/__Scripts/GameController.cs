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
    public GameObject unMuteBtn;
    public GameObject btnMute;
    private bool paused = false;
    public TextMeshProUGUI score;
    public Text playerName;
    public InputField input;
    public AudioSource song1;
    public AudioSource song2;
    private bool gameOver = false;
    public Slider slider;

    void Start()
    {
        playerName.text = PlayerPrefs.GetString("player_name");
        Debug.Log("Player name: "+playerName.text);
        ChangeGameVolume();
    }

    void Update()
    {

        //Check if the ecape key was pressed to pause
        if(Input.GetKeyDown(KeyCode.Escape) && gameOver == false)
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

        if(Input.GetKeyDown(KeyCode.M) && gameOver == false)
        {
            //Carry out function of Mute/UnMute Buttons
            if(btnMute.active)
            {
                song1.Pause();
                btnMute.SetActive(false);
                unMuteBtn.SetActive(true);
            }
            else
            {
                 song1.UnPause();
                 btnMute.SetActive(true);
                 unMuteBtn.SetActive(false);
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
        Enemy.EnemyKilledByPlayerEvent += HandleEnemyKilledByPlayerEvent;
    }

    private void OnDisable()
    {
        Enemy.EnemyKilledEvent -= HandleEnemyKilledEvent;
        Enemy.EnemyKilledByPlayerEvent -= HandleEnemyKilledByPlayerEvent;
    }

    public void HandleEnemyKilledEvent(Enemy enemy)
    {
        // add the score from the enemy to the player
        playerScore += enemy.ScoreValue;
        Debug.Log("Score: " + playerScore);
    }

    public void HandleEnemyKilledByPlayerEvent(Enemy enemy)
    {
        //Player only gets 2 points if used ship to hit rock
        playerScore += (enemy.ScoreValue/5);    
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        inGameUI.SetActive(false);
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

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        inGameUI.SetActive(true);
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

    public void RestartLevel()
    {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);    
    }

    public void SetGameVolume()
    {
        PlayerPrefs.SetString("GameVolume",slider.value.ToString());
        ChangeGameVolume();
    }

    public void ChangeGameVolume()
    {
        //Set Game volume to players preffered set volume
        float vol = float.Parse(PlayerPrefs.GetString("GameVolume"));
        
        Sound s1 = FindObjectOfType<AudioManager>().GetSound("hitmark");
        Sound s2 = FindObjectOfType<AudioManager>().GetSound("laser");
        Sound s3 = FindObjectOfType<AudioManager>().GetSound("explosions");
    
        s1.volume = vol;
        s2.volume = vol;
        s3.volume = vol;

        Debug.Log("New Volume:"+vol);
    }

    public void EndGame()
    {
        Debug.Log("GAME OVER!!!");
        PlayerPrefs.SetInt("player_score", playerScore);
        gameOver = true;

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
