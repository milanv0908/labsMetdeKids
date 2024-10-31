using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveKey : MonoBehaviour
{
    public float walkingSpeed = 1f;

    public enum InputType { KeyboardMouse }
    public InputType inputType;

    public Transform playerCamera;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Zorg ervoor dat het object niet draait door botsingen
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;

        if (inputType == InputType.KeyboardMouse)
        {
            // Haal invoer van toetsenbord op
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            // Verwerk camera-oriëntatie
            Vector3 cameraForward = playerCamera.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = playerCamera.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            // Pas beweging aan met walkingSpeed
            movement = (cameraForward * vertical + cameraRight * horizontal).normalized * walkingSpeed;
        }

        // Pas de snelheid van de Rigidbody direct aan
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }
}
