using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed = 15;
    /// <summary>
    /// Life time in seconds.
    /// </summary>
    [SerializeField]
    private float lifeTime = 3;

    private int damage = 1;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
        StartCoroutine(LifeTime());
    }

    /// <summary>
    /// Destroys the bullet automatically after its lifetime has past.
    /// </summary>
    /// <returns></returns>
    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyScript>().Hit(damage);
            ParticleManager.SpawnSparks(transform.position);
            Destroy(gameObject);
        }
    }
}
