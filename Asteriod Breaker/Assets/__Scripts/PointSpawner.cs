using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PointSpawner : MonoBehaviour
{
    // get a list of spawn points
    // randomly select one to get the position at which to start the enemy
    [SerializeField] private float spawnDelay = 1.0f;
    [SerializeField] private float spawnInterval = 1.5f;
    private int spawnChances = 10;
    private int playerScore = 0;

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Bomb bombPrefab;

    private GameObject enemyParent;
    private GameObject bombParent;

    [SerializeField] private GameObject phase1BckGrnd;
    [SerializeField] private GameObject phase2BckGrnd;
    [SerializeField] private GameObject phase3BckGrnd;
    [SerializeField] private GameObject phase4BckGrnd;

    private IList<SpawnPoint> spawnPoints;

    private Stack<SpawnPoint> spawnStack;

    // Start is called before the first frame update
    void Start()
    {
        // get the enemy parent
        enemyParent = ParentUtils.GetEnemyParent();
        // get the bomb parent
        bombParent = ParentUtils.GetBombParent();

        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        SpawnRepeating();
    }

    //Needed to keep track of score 
    //To scale difficulty
    private void Update()
    {
        playerScore = PlayerPrefs.GetInt("player_score", playerScore);
        if(playerScore > 50 && playerScore < 200)
        {
            //As more points are gained
            //Higher chance of a bomb to spawn
            spawnChances = 8;
            //Less time between enemies spawning
            spawnInterval = 1.0f;
            //Change the background color
            phase1BckGrnd.SetActive(false);
            phase2BckGrnd.SetActive(true);
        }
        else if(playerScore >= 200 && playerScore < 600)
        {
            spawnChances = 7;
            spawnInterval = 0.5f;
            phase2BckGrnd.SetActive(false);
            phase3BckGrnd.SetActive(true);
        }
        else if (playerScore >= 600 && playerScore < 1000)
        {
            spawnChances = 6;
            phase3BckGrnd.SetActive(false);
            phase4BckGrnd.SetActive(true);
        }
        else if(playerScore >= 1000)
        {
            spawnChances = 5;
            spawnInterval = 0.4f;
        }
    }

    private void SpawnRepeating()
    {
        // create a shuffled stack
        spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        InvokeRepeating("Spawn", spawnDelay, spawnInterval);
    }

    private void Spawn()
    {
        //var randomIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        //var currPoint = spawnPoints[randomIndex];
        // get the next item off the stack - pop
        // if stack is empty, reshuffle
        if( spawnStack.Count == 0)
        {
            spawnStack = ListUtils.CreateShuffledStack(spawnPoints);
        }
        var currPoint = spawnStack.Pop();

        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        var bomb = Instantiate(bombPrefab, bombParent.transform);

        //Randomly decided to spawn a rock (enemy) or a bomb
        //with a favour to spawning rocks
        System.Random rnd = new System.Random();
        int spawnDecider = rnd.Next(1,10);

        if(spawnDecider <= spawnChances)
            enemy.transform.position = currPoint.transform.position;
        else
            bomb.transform.position = currPoint.transform.position;
    }

}
