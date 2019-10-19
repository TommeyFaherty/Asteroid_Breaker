using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject playBtn;
    public InputField input;

    private void Start()
    {
        //Use listener to get name of player
        input.onEndEdit.AddListener(delegate {
            SubmitName(input);
        });
    }

    private void SubmitName(InputField userInput)
    {
        Debug.Log("Name: "+userInput.text);
        PlayerPrefs.SetString("player_name", userInput.text);
       
        //Ensure name is not blank
        if (userInput.text != "")
        {
            playBtn.SetActive(true);
        }
        else
        {
            playBtn.SetActive(false);
        }
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
 
    public void QuitGame()
    {
        Debug.Log("Quit was pressed");
        Application.Quit();
    }
}
