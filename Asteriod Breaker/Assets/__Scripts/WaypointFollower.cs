using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WaypointFollower : MonoBehaviour
{
    // == fields ==
    [SerializeField]
    private float speed = 2;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    // need a list of points to follow - set by DockSpawner
    private IList<Vector3> waypoints = new List<Vector3>();
    private Vector3 currentTarget;   // set that through a method
    private Rigidbody2D rb;
   
    // need a public method to add points
    public void AddPointToPath(Vector3 point)
    {
        waypoints.Add(point);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GetNextPoint();
    }

    private void GetNextPoint()
    {
        if(HasMorePoints())
        {
            currentTarget = waypoints.First();  // system.linq
        }
    }

    private bool HasMorePoints()
    {
        return waypoints.Count > 0;
    }

    private void FixedUpdate()
    {
        // move if there is a point to move to
        if(HasMorePoints())
        {
            MoveEnemy();
        }
        else
        {
            // do something here
            var falling = rb.GetComponent<FallingBehaviour>();
            falling.enabled = true;

        }
    }

    private void MoveEnemy()
    {
        // ge the next point on the list and move to it.
        // next point is the current point
        // rather than do maths, use Vector3 methods
        rb.position = Vector3.MoveTowards(rb.position,
                                          currentTarget,
                                          speed * Time.deltaTime);
        if(Vector3.Distance(rb.position, currentTarget) < 0.001)
        {
            // update the position of the rb to the current target
            // get the next point from the list
            rb.position = new Vector3(currentTarget.x, currentTarget.y);
            waypoints.Remove(currentTarget);
            if(HasMorePoints())
            {
                GetNextPoint();
            }
        }
    }
}
