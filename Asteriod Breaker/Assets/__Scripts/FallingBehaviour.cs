using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingBehaviour : MonoBehaviour
{
    // == private fields ==
    [SerializeField]
    private float speed = 2.5f;

    private int playerScore = 0;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // get the rigib body component 
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Increase the fall speed as the player score increases
        playerScore = PlayerPrefs.GetInt("player_score", playerScore);
        if(playerScore > 50 && playerScore < 200)
        {
             
            speed += 0.01f;
        }
        else if(playerScore >= 200 && playerScore < 600)
        {
            speed += 0.01f;
        }
        else if (playerScore >= 600 && playerScore < 1000)
        {
            speed += 0.05f;
        }
        else if(playerScore >= 1000)
        {
            speed += 0.05f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.down * speed;
    }
}
