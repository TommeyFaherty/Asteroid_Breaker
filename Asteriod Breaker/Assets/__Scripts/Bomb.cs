using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public delegate void BombKilled(Bomb enemy);

    public static BombKilled BombKilledEvent;
    // trigger event
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // use the tag to identify as a rectangle and not get
        // killed by bullet
        string tagType = gameObject.tag;

        var bullet = collision.GetComponent<Bullet>();
        var player = collision.GetComponent<PlayerMovement>();

        if (bullet && (tagType != "EnemyRectangle"))
        {
            // play a clip to inidcate a hit
            Destroy(bullet);
            PublishBombKilledEvent();
            Destroy(gameObject);

            //If hit End Game
            GameObject end = GameObject.Find("UICanvas");
            GameController gac = (GameController)end.GetComponent(typeof(GameController));
            gac.EndGame();
            
        }        
    }

    private void PublishBombKilledEvent()
    {
        BombKilledEvent?.Invoke(this);
    }
}
