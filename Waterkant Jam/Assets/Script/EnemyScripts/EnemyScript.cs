using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    protected int health = 5;
    protected float hitCooldown = 0.3f;
    protected float currentHitCooldown = float.MaxValue;

    /// <summary>
    /// Method called when the enemy gets hit.
    /// </summary>
    public virtual void Hit(int damage)
    {
        if (currentHitCooldown >= hitCooldown)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
                
                /*var o = Instantiate(trash, transform.position, Quaternion.identity);
                o.GetComponent<Rigidbody2D>().velocity = new Vector3(
                    Random.Range(-maxInitialTrashVelocity, maxInitialTrashVelocity),
                    Random.Range(-maxInitialTrashVelocity, maxInitialTrashVelocity),
                    Random.Range(-maxInitialTrashVelocity, maxInitialTrashVelocity));*/
            }
            StartCoroutine(CountDownHitCooldown());
        }
    }

    /// <summary>
    /// Counts down the hit cooldown.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountDownHitCooldown()
    {
        while (currentHitCooldown < hitCooldown)
        {
            currentHitCooldown += Time.deltaTime;
            yield return null;
        }
    }
}
