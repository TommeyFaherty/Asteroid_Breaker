using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class DockSpawner : MonoBehaviour
{
    // attached to the DockSpawner object
    // going to spawn rectangles, give them the path to follow
    // let them off
    // similar to the PointSpawner - need spawnDelay, spawnInterval, Spawn method
    [SerializeField]
    private float spawnDelay = 0.4f;

    [SerializeField]
    private float spawnInterval = 0.85f;

    [SerializeField]
    private float enemyStartSpeed = 2.0f;

    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    [Header("Path to Follow")]
    private Transform[] waypoints;


    private GameObject enemyParent;

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = ParentUtils.GetEnemyParent();
        SpawnRepeating();
    }

    private void SpawnRepeating()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnInterval);
    }

    private void Spawn()
    {
        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        enemy.transform.position = transform.position;

        // give the enemy object the points to follow
        var falling = enemy.GetComponent<FallingBehaviour>();
        falling.enabled = false;
        // need a public property on the WaypointFollower
        var follower = enemy.GetComponent<WaypointFollower>();
        follower.Speed = enemyStartSpeed;
        foreach (var transform in waypoints)
        {
            follower.AddPointToPath(transform.position);
        }
    }
}
