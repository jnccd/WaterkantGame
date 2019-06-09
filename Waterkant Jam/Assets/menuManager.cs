using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButton("ControllerShoot"))
        {
            LoadGame();
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
}
