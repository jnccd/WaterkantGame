using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5;
    public GameObject bulletPrefab;
    [SerializeField]
    private float cooldownTime = 0.15f;
    private float currentCooldown = 0;

    private void FixedUpdate()
    {
        // Get current input.
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 controllerInput = new Vector2(Input.GetAxis("ControllerHorizontal"), Input.GetAxis("ControllerVertical"));
        input += controllerInput;
        GetComponent<Rigidbody2D>().velocity = input * playerSpeed * Time.deltaTime * 60;
    }

    private void Update()
    {
        if (currentCooldown < cooldownTime)
        {
            currentCooldown += Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Space) ||Input.GetButton("ControllerShoot"))
        {
            currentCooldown = 0;
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }
}
