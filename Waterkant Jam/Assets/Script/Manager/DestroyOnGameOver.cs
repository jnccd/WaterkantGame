using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnGameOver : MonoBehaviour
{
    private void Start()
    {
        GameManager.GameOver += DestroySelf;
    }

    private void DestroySelf()
    {
        GameManager.GameOver -= DestroySelf;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.GameOver -= DestroySelf;
    }
}
