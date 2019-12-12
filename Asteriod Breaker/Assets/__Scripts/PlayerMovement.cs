using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    // == public fields ==
    // == private fields ==
    [SerializeField]
    private float moveSpeed = 7.0f;
    [SerializeField]
    private Sprite GoSprite;
    [SerializeField]
    private Sprite DefaultSprite;
    private Dictionary<string,KeyCode> keys = new Dictionary<string, KeyCode>();
    [SerializeField] private Button controlsSaveBtn;

    void Start()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Up","W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Down","S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Left","A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Right","D")));

        //If player clicks save in binding menus
        //Controls will update
        controlsSaveBtn.onClick.AddListener(UpdateKeys);
    }
    // == private methods ==
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 position = this.transform.position;

        if(Input.GetKey(keys["Up"]))
        {
           transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        } 

        if(Input.GetKey(keys["Down"]))
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }   

        if(Input.GetKey(keys["Left"]))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        if(Input.GetKey(keys["Right"]))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        //Changes sprite when moveing along the Y axis
        if(Input.GetKey(keys["Up"]))
        {
            this.GetComponent<SpriteRenderer>().sprite = GoSprite;
        }
        else if(this.GetComponent<SpriteRenderer>().sprite == GoSprite)
        {
            this.GetComponent<SpriteRenderer>().sprite = DefaultSprite;
        }
    }

    private void UpdateKeys()
    {
        keys["Up"] = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Up","W"));
        keys["Down"] = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Down","S"));
        keys["Left"] = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Left","A"));
        keys["Right"] = (KeyCode)System.Enum.Parse(typeof(KeyCode),PlayerPrefs.GetString("Right","D"));
    }
}
