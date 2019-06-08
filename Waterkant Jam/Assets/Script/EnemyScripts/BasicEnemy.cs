using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemy : EnemyScript
{
    [SerializeField]
    private float initialThrust = 2.5f;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-initialThrust, 0);
    }
}
