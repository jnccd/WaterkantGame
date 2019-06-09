using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private PowerUpUIManager powerUpUIManager;

    public float playerSpeed = 5;
    public GameObject bulletPrefab;
    [SerializeField]
    private GameObject cannonBulletPrefab;
    [SerializeField]
    private float cannonPowerTime;
    private float cannonStartTime = 0;
    [SerializeField]
    private float canonCooldownTime = 1;

    [SerializeField]
    private float cooldownTime = 0.15f;
    private float timerOnLastShoot = 0;

    private float timer = 0;

    private bool canMove = true;

    [SerializeField]
    private Shield shield;
    public bool isShielded = false;

    [SerializeField]
    private GameObject cannon;

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject radioEmp;

    [SerializeField]
    private GameObject trashPrefab;

    private PowerUpScript.PowerUpType currentPowerUp = PowerUpScript.PowerUpType.None;

    private bool cannonInUse = false;

    private void Awake()
    {
        GameManager.GameStart += OnGameStart;
        GameManager.GameOver += OnGameOver;
    }

    private void OnGameStart()
    {
        ToogleShield(false);
        cannonInUse = false;
    }

    private void OnGameOver()
    {
        ToogleShield(false);
        cannonInUse = false;
    }

    public void ToogleShield(bool newBool)
    {
        isShielded = newBool;
        shield.gameObject.SetActive(newBool);
        if (newBool)
        {
            shield.ResetShield();
        }
    }
    
    private void FixedUpdate()
    {
        // Get current input.
        if (canMove)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 controllerInput = new Vector2(Input.GetAxis("ControllerHorizontal"), Input.GetAxis("ControllerVertical"));
            input += controllerInput;
            GetComponent<Rigidbody2D>().velocity = input * playerSpeed * Time.deltaTime * 60;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("ControllerShoot")))
        {
            if (cannonInUse)
            {
                if (timer - timerOnLastShoot > canonCooldownTime)
                {
                    Instantiate(cannonBulletPrefab, transform.position, Quaternion.identity);
                    timerOnLastShoot = timer;
                }

                if (timer - cannonStartTime > cannonPowerTime)
                {
                    cannonInUse = false;
                    cannon.gameObject.SetActive(false);
                    SpawnTrash();
                }
            }
            else
            {
                if (timer - timerOnLastShoot > cooldownTime)
                {
                    Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    timerOnLastShoot = timer;
                }
            }
        }

        if (currentPowerUp != PowerUpScript.PowerUpType.None && (Input.GetKeyDown(KeyCode.E) || Input.GetButton("ControllerPowerUp")))
        {
            // TODO: Use Power up.
            switch (currentPowerUp)
            {
                case PowerUpScript.PowerUpType.Cannon:
                    cannonInUse = true;
                    cannonStartTime = timer;
                    SoundManager.instance.PlayerSound(SoundManager.instance.Cannon1);
                    cannon.gameObject.SetActive(true);
                    break;
                case PowerUpScript.PowerUpType.Shield:
                    SoundManager.instance.PlayerSound(SoundManager.instance.Construction);
                    ToogleShield(true);
                    SpawnTrash();
                    break;
                case PowerUpScript.PowerUpType.Explosion:
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    CameraScript.camera.SetCameraShake(0.6f, 1.5f);
                    SoundManager.instance.PlayerSound(SoundManager.instance.Explosion1);
                    SpawnTrash();
                    break;
                case PowerUpScript.PowerUpType.Electro:
                    GameObject radioEffect = Instantiate(radioEmp, transform.position, Quaternion.identity);
                    Destroy(radioEffect, 1);
                    SoundManager.instance.PlayerSound(SoundManager.instance.Radio);
                    SpawnTrash();
                    break;
            }
            currentPowerUp = PowerUpScript.PowerUpType.None;

            powerUpUIManager.ClosePanel();
        }
    }

    public void SetCanMove(bool shallThePlayerBeAbleTOMove)
    {
        canMove = shallThePlayerBeAbleTOMove;
        if (shallThePlayerBeAbleTOMove == false)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }
    }

    private void SpawnTrash()
    {
        Instantiate(trashPrefab, transform.position + Vector3.right * 2.5f, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            if (currentPowerUp == PowerUpScript.PowerUpType.None)
            {
                SoundManager.instance.PlayerSound(SoundManager.instance.Aura);
                currentPowerUp = collision.gameObject.GetComponent<PowerUpScript>().myType;
                powerUpUIManager.OpenPanel(collision.GetComponent<SpriteRenderer>().sprite);
                Destroy(collision.gameObject);
            }
        }
    }
}