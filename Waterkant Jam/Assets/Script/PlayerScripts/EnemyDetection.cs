using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyBullet"))
        {
            SoundManager.instance.PlayerSound(SoundManager.instance.Death);
            if (!playerMovement.isShielded)
            {
                gameManager.GameWasLost();
                playerMovement.SetCanMove(false);
                //SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
