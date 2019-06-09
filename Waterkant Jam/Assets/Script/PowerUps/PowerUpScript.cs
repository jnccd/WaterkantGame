using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public enum PowerUpType {Cannon, Shield, Explosion, Electro, None}
    [SerializeField]
    public PowerUpType myType;

    private void Update()
    {
        Vector3 position = transform.position + Vector3.left * Time.deltaTime * 2;
        transform.position = position;
    }
}
