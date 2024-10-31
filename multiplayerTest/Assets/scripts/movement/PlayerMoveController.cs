using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float walkingSpeed = 1.0f;

    public enum InputType { KeyboardMouse, Controller }
    public InputType inputType;

    public Transform playerCamera2;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Zorg ervoor dat het object niet roteert door botsingen
    }

    void FixedUpdate()
    {
        Vector3 movement = Vector3.zero;

        if (inputType == InputType.Controller)
        {
            // Controller movement logic
            float horizontal = Input.GetAxis("ControllerHorizontal");
            float vertical = Input.GetAxis("ControllerVertical");

            // Drempelwaarde om onbedoelde beweging te voorkomen
            float deadZone = 0.6f;

            // Controleer invoer boven de dead zone
            if (Mathf.Abs(horizontal) > deadZone || Mathf.Abs(vertical) > deadZone)
            {
                // Oriëntatie camera
                Vector3 cameraForward = playerCamera2.forward;
                cameraForward.y = 0;
                cameraForward.Normalize();

                Vector3 cameraRight = playerCamera2.right;
                cameraRight.y = 0;
                cameraRight.Normalize();

                // Beweging aangepast aan camera met walking speed
                movement = (cameraForward * vertical + cameraRight * horizontal).normalized * walkingSpeed;
            }
        }

        // Pas de snelheid van de Rigidbody aan voor beweging
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        
    }
}
