using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockCollider : MonoBehaviour
{
    [SerializeField]
    private float freezeDuration = 3;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<ShockScript>().Freeze(freezeDuration);
        }
    }
}
