using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyScript
{
    [SerializeField]
    private GameObject trash;

    [SerializeField]
    private Transform spawnPos;

    private bool started = false;


    private void Start()
    {

        SoundManager.instance.PlayerSound(SoundManager.instance.BossIntro);
        StartCoroutine(StartDelay());
        SoundManager.instance.PlayBossMusic();
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1.5f);
        started = true;
    }


    public void SpawnTrash()
    {
        if(started)
             Instantiate(trash, spawnPos.position, Quaternion.identity);
    }

    public override void Hit(int damage)
    {
        if (health % 10 == 0)
        {
            SoundManager.instance.PlayerSound(SoundManager.instance.BossHit);
        }
        base.Hit(damage);
    }
}
