using System;
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
    [SerializeField]
    private GameObject trashEnemyPrefab;
    [SerializeField]
    private GameObject bossEnemyPrefab;

    [SerializeField]
    private GameObject[] powerUps;


    private bool gameIsRunning = false;

    private bool spawnEnemies = true;

    float timer = 0;
    float phaseStartTime = 0;
    int phaseIndex;
    private float introGab;


    public class Phase
    {
        public int duration;
        public Action coroutines;

        public Phase(int duration, Action coroutines)
        {
            this.duration = duration;
            this.coroutines = coroutines;
        }
    }

    Phase[] phases;

    private void Awake()
    {
        GameManager.GameOver += OnGameOver;

        GameManager.GameStart += RestartLevelManager;
        GameManager.GameOver += EndLevelManager;

        phases = new Phase[]
        {
            new Phase(10, () => {
            }),
            new Phase(12, () => {
                StartCoroutine(SpawnEnemyCorutine(() => SpawnEnemy(basicEnemyPrefab), 5, 12));
            }),
            new Phase(22, () => {
                StartCoroutine(SpawnEnemyCorutine(() => SpawnEnemy(basicEnemyPrefab), 3, 22));
                StartCoroutine(SpawnEnemyCorutine(() => StartCoroutine(SpawnMetalGearEnemyGroup(3)), 5, 22));
                StartCoroutine(SpawnPowUpCorutine(10, 22));
            }),
            new Phase(16, () => {
                StartCoroutine(SpawnEnemyCorutine(() => SpawnEnemy(basicEnemyPrefab), 1.5f, 16));
                StartCoroutine(SpawnEnemyCorutine(() => StartCoroutine(SpawnMetalGearEnemyGroup(4)), 4, 16));
                StartCoroutine(SpawnEnemyCorutine(() => SpawnEnemy(trashEnemyPrefab), 6, 16));
                StartCoroutine(SpawnPowUpCorutine(10, 16));
            }),
            new Phase(17, () => {
                StartCoroutine(SpawnEnemyCorutine(() => SpawnEnemy(beamEnemyPrefab), 6, 17));
                StartCoroutine(SpawnEnemyCorutine(() => StartCoroutine(SpawnMetalGearEnemyGroup(5)), 5, 17));
                StartCoroutine(SpawnEnemyCorutine(() => SpawnEnemy(trashEnemyPrefab), 5, 17));
                StartCoroutine(SpawnPowUpCorutine(10, 17));
            }),
            new Phase(25, () => {
                StartCoroutine(SpawnEnemyCorutine(() => SpawnEnemy(beamEnemyPrefab), 3, 25));
                StartCoroutine(SpawnEnemyCorutine(() => StartCoroutine(SpawnMetalGearEnemyGroup(5)), 4, 25));
                StartCoroutine(SpawnEnemyCorutine(() => SpawnEnemy(trashEnemyPrefab), 4, 25));
                StartCoroutine(SpawnPowUpCorutine(10, 25));
            }),
            new Phase(10, () => {
            }),
            new Phase(20, () => {
                StartCoroutine(SpawnEnemyCorutine(() => SpawnEnemy(bossEnemyPrefab), 10, 1));
            }),
        };
        phases[0].coroutines();
    }

    private void OnGameOver() => phaseIndex = 0;


    /// <summary>
    /// Called when the game starts.
    /// </summary>
    private void RestartLevelManager()
    {
        gameIsRunning = true;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (phases[phaseIndex].duration < timer - phaseStartTime)
        {
            Debug.Log("change to phase: " + ++phaseIndex);

            phaseStartTime = timer;
            phases[phaseIndex].coroutines();
        }
    }

    /// <summary>
    /// Called when the game was lost.
    /// </summary>
    private void EndLevelManager()
    {
        gameIsRunning = false;
    }

    private IEnumerator SpawnEnemyCorutine(Action Enemyspawn, float IntervalInSeconds, float duration)
    {
        float startTime = timer;
        while (timer - startTime < duration)
        {
            if (spawnEnemies)
                Enemyspawn();
            yield return new WaitForSeconds(IntervalInSeconds);
        }
    }

    private void SpawnEnemy(GameObject EnemyPrefab)
    {
        Vector3 spawnPos = spawnPosition.position;
        spawnPos.y += UnityEngine.Random.Range(-yOffset, yOffset);
        Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
    }

    private IEnumerator SpawnMetalGearEnemyGroup(int number)
    {
        Vector3 spawnPos = spawnPosition.position;
        spawnPos.y += UnityEngine.Random.Range(-yOffset, yOffset);
        for (int i = 0; i < number; i++)
        {
            Instantiate(metalGearEnemyPrefab, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(0.15f);
        }
    }

    private IEnumerator SpawnPowUpCorutine(float IntervalInSeconds, float duration)
    {
        float startTime = timer;
        while (timer - startTime < duration)
        {
            if (spawnEnemies)
                SpawnPowerUp();
            yield return new WaitForSeconds(IntervalInSeconds);
        }
    }

    private void SpawnPowerUp()
    {
        Vector3 spawnPos = spawnPosition.position;
        spawnPos.y += UnityEngine.Random.Range(-yOffset, yOffset);
        int ran = UnityEngine.Random.Range(0, powerUps.Length);
        Instantiate(powerUps[ran], spawnPos, Quaternion.identity);
    }
}
