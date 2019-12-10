using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject playBtn;
    public Slider slider;
    public InputField input;
    public Text up, down, left, right, shoot;

    public Dictionary<string,KeyCode> keys = new Dictionary<string, KeyCode>();

    private void Start()
    {
        //Use listener to get name of player
        input.onEndEdit.AddListener(delegate {
            SubmitName(input);
        });

        //Get or initialise all keys current values
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Up","W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Down","S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Left","A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Right","D")));
        keys.Add("Shoot", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Shoot","Space")));

        //Set all text values of buttons to currently set keys
        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
        shoot.text = keys["Shoot"].ToString();
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

    public void ChangeGameSound()
    {
        PlayerPrefs.SetString("GameVolume",slider.value.ToString());
    }
}
