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
}
