using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // == public fields ==
    // == private fields ==
    [SerializeField]
    private float moveSpeed = 7.0f;

    // == private methods ==
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        // find the change signal from the keyboard/input device
        // create a value for the change
        // Time.deltaTime - frame rate independent - same experience
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        // add the change to the current position
        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);
    }
}
