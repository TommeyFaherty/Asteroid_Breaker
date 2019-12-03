using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // use later for other stuff
    // add the collider stuff, to detect if a bullet hits
    // detect the collisiion
    // destroy the bullet prefab, then this prefab
    [SerializeField] private int scoreValue = 10;

    public int ScoreValue { get { return scoreValue; } }

    // when the enemy dies, need to give the player the points
    // could find the player object, and add the points there
    // create a game controller - manage the score
    // subscribe to an event that the enemy kicks off when it dies
    // use a delegate type to create the event

    public delegate void EnemyKilled(Enemy enemy);

    public static EnemyKilled EnemyKilledEvent;

    // trigger event
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // use the tag to identify as a rectangle and not get
        // killed by bullet
        string tagType = gameObject.tag;

        var bullet = collision.GetComponent<Bullet>();
        var player = collision.GetComponent<PlayerMovement>();

        if(bullet && (tagType != "EnemyRectangle"))
        {
            // play a clip to inidcate a hit
            Destroy(bullet);
            PublishEnemyKilledEvent();
            Destroy(gameObject);
        }
        else if (player && tagType == "EnemyRectangle")
        {
            // play a crash clip
            PublishEnemyKilledEvent();
            Destroy(gameObject);
        }
    }

    // need to publish the dying event to the system
    private void PublishEnemyKilledEvent()
    {
        EnemyKilledEvent?.Invoke(this);
    }
}
