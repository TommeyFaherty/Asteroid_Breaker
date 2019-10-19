using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dock : MonoBehaviour
{
    [SerializeField]
    private float Height = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,
                            new Vector3(Height, 0.5f));
    }
}
