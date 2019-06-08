using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private int health = 5;
    private float hitCooldown = 0.3f;
    private float currentHitCooldown = float.MaxValue;

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
