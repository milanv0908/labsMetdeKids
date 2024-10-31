using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float lookSpeed = 3.0f;
    private Vector2 rotation = Vector2.zero;

    public Transform playerCamera; // Voeg dit toe om handmatig de camera te koppelen

    public enum InputType { KeyboardMouse, Controller }
    public InputType inputType; // Dit moet publiek zijn

    void Start()
    {
        Cursor.visible = false;

        if (playerCamera == null)
        {
            Debug.LogError("Camera not assigned! Please assign the camera in the Inspector.");
        }
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (inputType == InputType.KeyboardMouse)
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        }

           if (inputType == InputType.Controller)
        {
            rotation.y += Input.GetAxis("RightStickHorizontal");
            rotation.x += -Input.GetAxis("RightStickVertical");
            rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        }

        // Beweging met controller uitschakelen
        // Voeg hier geen code toe voor controller-invoer.

        transform.eulerAngles = new Vector2(0, rotation.y) * lookSpeed;

        if (playerCamera != null)
        {
            playerCamera.localRotation = Quaternion.Euler(rotation.x * lookSpeed, 0, 0);
        }
    }
}
