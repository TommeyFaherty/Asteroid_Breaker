using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    // get the main camera
    // get the bounds of it (X,Y) - half the size
    // use Mathf.Clamp to compare the min, max to the current x

    // == public properties ==
    public Camera mainCamera;

    // == private fields ==
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        // find the bounds from the camera
        screenBounds = mainCamera.ScreenToWorldPoint(
                                new Vector3(Screen.width,
                                            Screen.height,
                                            mainCamera.transform.position.z));
        Debug.Log("Screen width = " + screenBounds.x);
        objectWidth = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    // after the player moves, need to check that they are in bounds
    // player moves on the update method.  Can't use the update - need to 
    // ensure this is done after Update -> LateUpdate
    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        // find the position of X and clamp
        // find the position of Y and clamp
        viewPos.x = Mathf.Clamp(viewPos.x, 
                                screenBounds.x * -1 + objectWidth, 
                                screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, 
                                screenBounds.y * -1 + (objectHeight * 0.8f), 
                                screenBounds.y - objectHeight);

        transform.position = viewPos;
    }
}
