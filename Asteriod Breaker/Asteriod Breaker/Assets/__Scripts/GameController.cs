using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // == fields ==
    private int playerScore = 0;
    public GameObject pausePanel;
    private bool paused = false;
    public TextMeshProUGUI score;
    public Text playerName;
    public InputField input;

    void Start()
    {
        playerName.text = PlayerPrefs.GetString("player_name");
         
        Debug.Log("Player name: "+playerName.text);
    }

    void Update()
    {
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
        score.text = playerScore.ToString();
        
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
    }

    private void Quit()
    {
        //Time Scale is reverted back to normal here as it is a global value
        //and would remain 0 otherwise
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
