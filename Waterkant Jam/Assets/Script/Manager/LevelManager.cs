using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPosition;
    [Tooltip("The amount that the y coordinate of a spawn position con deviate")]
    [SerializeField]
    private float yOffset;

    [SerializeField]
    private GameObject basicEnemyPrefab;
    [SerializeField]
    private GameObject metalGearEnemyPrefab;
    [SerializeField]
    private GameObject beamEnemyPrefab;

    private bool spawnEnemies = true;

    private void Start()
    {
        StartCoroutine(SpawnEnemyCorutine());
        StartCoroutine(SpawnBasicEnemyCorutine());
    }

    private void Update()
    {
        
    }

    private IEnumerator SpawnEnemyCorutine()
    {
        while (true)
        {
            if (spawnEnemies)
            {
                StartCoroutine(SpawnMetalGearEnemyGroup(5));
            }
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator SpawnBasicEnemyCorutine()
    {
        while (true)
        {
            if (spawnEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(2f);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPosition.position;
        spawnPos.y += Random.Range(-yOffset, yOffset);
        Instantiate(basicEnemyPrefab, spawnPos, Quaternion.identity);
    }

    private void SpawnMetalGearEnemy()
    {
        Vector3 spawnPos = spawnPosition.position;
        spawnPos.y += Random.Range(-yOffset, yOffset);
        Instantiate(metalGearEnemyPrefab, spawnPos, Quaternion.identity);
    }

    private IEnumerator SpawnMetalGearEnemyGroup(int number)
    {
        Vector3 spawnPos = spawnPosition.position;
        spawnPos.y += Random.Range(-yOffset, yOffset);
        for (int i = 0; i < number; i++)
        {
            Instantiate(metalGearEnemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);
        }
    }
}
