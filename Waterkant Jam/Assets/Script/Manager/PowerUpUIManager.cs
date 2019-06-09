using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUIManager : MonoBehaviour
{
    [SerializeField]
    private Image powerUpImage;
    [SerializeField]
    private Image powerUpPanel;

    private void Awake()
    {
        GameManager.GameStart += ClosePanel;
        GameManager.GameOver += ClosePanel;
    }

    public void OpenPanel(Sprite powerUpSprite)
    {
        powerUpPanel.gameObject.SetActive(true);
        powerUpImage.sprite = powerUpSprite;
    }

    public void ClosePanel()
    {
        powerUpPanel.gameObject.SetActive(false);
    }
}
