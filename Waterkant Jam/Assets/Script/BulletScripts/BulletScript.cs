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
    [SerializeField]
    private bool destroyOnContact;
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private bool destroyAfterLifeTime = true;

    protected virtual void Start()
    {
        if(bulletSpeed != 0)
            GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
        if(destroyAfterLifeTime)
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SoundManager.instance.PlayerSound(SoundManager.instance.Hit);
            collision.gameObject.GetComponent<EnemyScript>().Hit(damage);
            ParticleManager.SpawnSparks(transform.position);
            if (destroyOnContact)
            {
                Destroy(gameObject);
            }
        }
    }
}
