using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockScript : MonoBehaviour
{
    private EnemyScript normalScript;

    private Vector2 velocityAtShock;

    private void Start()
    {
        normalScript = GetComponent<EnemyScript>();
    }

    public void Freeze(float duration)
    {
        normalScript.enabled = false;
        velocityAtShock = GetComponent<Rigidbody2D>().velocity;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(countdownShockTime(duration));
    }

    private IEnumerator countdownShockTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        GetComponent<Rigidbody2D>().velocity = velocityAtShock;
        normalScript.enabled = true;
    }
}
