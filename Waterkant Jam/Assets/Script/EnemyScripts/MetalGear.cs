using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A solid class.
/// </summary>
public class MetalGear : EnemyScript
{
    Vector3 basePos;
    readonly float dis = 1.5f;
    private float timer = 0;
    private readonly float multiplier = 2f;
    private float enemySpeed = 2;

    private float rotationSpeed = 270;
    private float rotation = 0;

    private void Start()
    {
        basePos = transform.position;
        UpdatePos();
    }

    private void Update()
    {
        rotation += rotationSpeed * Time.deltaTime;
        rotation = rotation % 360;
        transform.rotation = Quaternion.Euler(0, 0, rotation);

        timer += Time.deltaTime;
        basePos.x -= enemySpeed * Time.deltaTime;
        UpdatePos();
    }
    void UpdatePos()
    {
        transform.position = new Vector3(
            basePos.x + Mathf.Cos(timer * multiplier) * dis,
            basePos.y + Mathf.Sin(timer * multiplier) * dis, 0);
    }
}