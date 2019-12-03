using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameMenus : MonoBehaviour
{
    public GameObject playAgainBtn;
    private bool paused = false;
    public GameObject gameOverPanel;
    public GameObject inGameUI;
    public Bomb bombs;
    private bool isShot = false;

    private void Update()
    {

    }

    private void EndGame()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        inGameUI.SetActive(false);
        paused = true;

        //Use listener to get name of player
        /*input.onEndEdit.AddListener(delegate {
            SubmitName(input);
        });*/
    }

    public void QuitGame()
    {
        Debug.Log("Quit was pressed");
        Application.Quit();
    }
}
