using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for the overall flow of the game.
/// </summary>
public class GameManager : MonoBehaviour
{

    public delegate void GameEvent();
    public static event GameEvent GameOver;
    public static event GameEvent GameStart;

    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private Image sliderPanel;
    [SerializeField]
    private Slider progressionSlider;
    [SerializeField]
    private float gameTime;

    private bool gameIsRunning = true;

    private void Start()
    {
        gameOverScreen.gameObject.SetActive(false);
        progressionSlider.maxValue = gameTime;
        StartGame();
    }

    /// <summary>
    /// (Re) Starts the game.
    /// </summary>
    public void StartGame()
    {
        progressionSlider.value = 0;
        playerTransform.position = Vector3.left * 2;
        playerTransform.GetComponent<PlayerMovement>().SetCanMove(true);
        gameIsRunning = true;
        gameOverScreen.gameObject.SetActive(false);

        if (GameStart != null)
        {
            GameStart();
        }
    }

    private void Update()
    {
        progressionSlider.value = progressionSlider.value + Time.deltaTime;
        if (progressionSlider.value >= gameTime)
        {
            // Call for boss.
        }

        if (gameIsRunning == false && (Input.GetKeyDown(KeyCode.R) || Input.GetButton("ControllerReset")))
        {
            StartGame();
        }
    }

    public void GameWasLost()
    {
        if (GameOver != null)
        {
            GameOver();
        }
        gameOverScreen.gameObject.SetActive(true);
        gameIsRunning = false;
    }
}
