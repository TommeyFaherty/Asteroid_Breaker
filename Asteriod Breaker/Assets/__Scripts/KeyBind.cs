using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBind : MonoBehaviour
{
    private Dictionary<string,KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text up, down, left, right, shoot;

    private GameObject currentKey;

    private Color32 normal = new Color32(39, 171, 249, 255);
    private Color32 slected = new Color32(239, 116, 36, 255);

    // Start is called before the first frame update
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keys["Up"]))
        {
            Vector3 position = this.transform.position;
            position.y++;
            this.transform.position = position;
        } 

        if(Input.GetKeyDown(keys["Down"]))
        {
            Vector3 position = this.transform.position;
            position.y--;
            this.transform.position = position;
        }   

        if(Input.GetKeyDown(keys["Left"]))
        {
            Vector3 position = this.transform.position;
            position.x--;
            this.transform.position = position;
        }

        if(Input.GetKeyDown(keys["Right"]))
        {
            Vector3 position = this.transform.position;
            position.x++;
            this.transform.position = position;
        }
    }

    void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if(e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        if(currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = slected;
    }

    public void SaveKeys()
    {
        foreach(var key in keys)
        {
            PlayerPrefs.SetString(key.Key,key.Value.ToString());
        }

        PlayerPrefs.Save();
    }
}
