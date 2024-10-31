using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player1; // Sleep hier je eerste speler in de inspector
    public GameObject player2; // Sleep hier je tweede speler in de inspector
    public Transform playerCamera; // Voeg deze regel toe om de camera te koppelen
    public Transform playerCamera2; // Voeg deze regel toe om de camera te koppelen

    void Start()
    {
        SetupPlayers();
    }

    void SetupPlayers()
    {
        // PlayerMoveKey en PlayerLook voor player1
        PlayerMoveKey playerMove1 = player1.GetComponent<PlayerMoveKey>();
        PlayerLook playerLook1 = player1.GetComponent<PlayerLook>();
        playerMove1.inputType = PlayerMoveKey.InputType.KeyboardMouse; // Alleen toetsenbord/muis
        playerMove1.playerCamera = playerCamera; // Koppel de camera aan de speler
        playerLook1.inputType = PlayerLook.InputType.KeyboardMouse; // Alleen toetsenbord/muis

        // PlayerMoveController en PlayerLook voor player2
        PlayerMoveController playerMove2 = player2.GetComponent<PlayerMoveController>();
        PlayerLook playerLook2 = player2.GetComponent<PlayerLook>();
        playerMove2.inputType = PlayerMoveController.InputType.Controller; // Alleen controller
        playerMove2.playerCamera2 = playerCamera2; // Koppel de camera aan de speler
        playerLook2.inputType = PlayerLook.InputType.Controller; // Alleen controller
    }
}
