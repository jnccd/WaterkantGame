using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BulletScript
{
    [SerializeField]
    private PlayerMovement playerMovement;
    int testShieldHP = 3;

    protected override void Start()
    {
        ResetShield();
    }

    public void ResetShield()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        testShieldHP = 3;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            SoundManager.instance.PlayerSound(SoundManager.instance.Brechen);
            Destroy(collision.gameObject);
            ParticleManager.SpawnSparks(transform.position);

            testShieldHP--;
            if (testShieldHP == 2)
                GetComponent<SpriteRenderer>().color = Color.yellow;
            else if (testShieldHP == 1)
                GetComponent<SpriteRenderer>().color = Color.red;
            else
            {
                playerMovement.ToogleShield(false);
                ResetShield();
            }
        }
    }
}
