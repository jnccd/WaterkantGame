using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashEnemy : EnemyScript
{
    [SerializeField]
    private float initialThrust = 3f;

    private float maxInitialTrashVelocity = 8;
    [SerializeField]
    private GameObject trash;

    private void Start()
    {
        health = 2;
        GetComponent<Rigidbody2D>().velocity = new Vector2(-initialThrust, 0);
    }

    /// <summary>
    /// Method called when the enemy gets hit.
    /// </summary>
    public override void Hit(int damage)
    {
        if (health - damage <= 0)
        {
            var o = Instantiate(trash, transform.position, Quaternion.identity);
            /*o.GetComponent<Rigidbody2D>().velocity = new Vector3(
                Random.Range(-maxInitialTrashVelocity, maxInitialTrashVelocity),
                Random.Range(-maxInitialTrashVelocity, maxInitialTrashVelocity),
                Random.Range(-maxInitialTrashVelocity, maxInitialTrashVelocity));*/
        }

        base.Hit(damage);
    }
}
