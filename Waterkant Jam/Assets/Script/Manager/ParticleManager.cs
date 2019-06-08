using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spark;
    private static GameObject sparkPrefab;

    private void Awake()
    {
        sparkPrefab = spark;
    }

    public static void SpawnSparks(Vector3 spawnPos)
    {
        GameObject newEffect = Instantiate(sparkPrefab, spawnPos, Quaternion.identity);
        Destroy(newEffect, 1);
    }
}
