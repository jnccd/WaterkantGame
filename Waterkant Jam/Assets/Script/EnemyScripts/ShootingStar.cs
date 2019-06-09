using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : EnemyScript
{
    [SerializeField]
    private Transform upperPart;
    [SerializeField]
    private Transform lowerPart;
    [SerializeField]
    private GameObject enemyBulletPrefab;
    [SerializeField]
    private GameObject emision;
    [SerializeField]
    private GameObject bulletLoadUp;

    float currentAngle = 0;
    float rotationSpeed = 200;
    private float initialWait = 1;
    private bool shooting = false;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1f, 0);
        StartCoroutine(Open());
        bulletLoadUp.SetActive(false);
        emision.SetActive(false);
    }
    

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(1);
        while (currentAngle < 44)
        {
            currentAngle += Time.deltaTime * rotationSpeed;
            upperPart.rotation = Quaternion.Euler(0,0, -currentAngle);
            lowerPart.rotation = Quaternion.Euler(0, 0, currentAngle);
            yield return null;
        }
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        shooting = true;
        while (shooting)
        {
            bulletLoadUp.SetActive(true);
            yield return new WaitForSeconds(0.4f);
            emision.SetActive(true);
            yield return new WaitForSeconds(1.2f);
            bulletLoadUp.SetActive(false);
            emision.SetActive(false);
            Instantiate(enemyBulletPrefab, transform.position + Vector3.left * 0.8f, Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        while (currentAngle > 0)
        {
            currentAngle -= Time.deltaTime * rotationSpeed;
            upperPart.rotation = Quaternion.Euler(0, 0, -currentAngle);
            lowerPart.rotation = Quaternion.Euler(0, 0, currentAngle);
            yield return null;
        }
    }
}
