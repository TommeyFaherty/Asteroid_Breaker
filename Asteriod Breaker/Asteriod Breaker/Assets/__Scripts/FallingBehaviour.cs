using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallingBehaviour : MonoBehaviour
{
    // == private fields ==
    [SerializeField]
    private float speed = 2.5f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // get the rigib body component 
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.down * speed;
    }
}
