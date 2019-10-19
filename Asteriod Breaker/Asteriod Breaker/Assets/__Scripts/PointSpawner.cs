using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PointSpawner : MonoBehaviour
{
    // get a list of spawn points
    // randomly select one to get the position at which to start the enemy
    [SerializeField]
    private float spawnDelay = 1.0f;

    [SerializeField]
    private float spawnInterval = 0.5f;

    [SerializeField]
    private Enemy enemyPrefab;

    private GameObject enemyParent;

    private IList<SpawnPoint> spawnPoints;

    private Stack<SpawnPoint> spawnStack;

    // Start is called before the first frame update
    void Start()
    {
        // get the enemy parent
        enemyParent = ParentUtils.GetEnemyParent();
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        SpawnRepeating();
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
        enemy.transform.position = currPoint.transform.position;
    }

}
