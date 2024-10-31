using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionHandler : MonoBehaviour
{
    public Transform player; // De speler positie om de camera naar terug te trekken
    public float smoothSpeed = 5f; // Snelheid voor soepele overgang
    public float minCollisionOffset = 0.5f; // De minimale afstand bij botsing
    public float maxCollisionOffset = 1.5f; // De maximale afstand bij botsing
    public float heightOffset = 1.5f; // De hoogte waar de camera zich moet bevinden

    private Vector3 originalLocalPosition; // De originele positie van de camera in de parent
    private bool isColliding = false; // Houdt bij of er een botsing is
    private float currentCollisionOffset; // Huidige offset bij botsing

    void Start()
    {
        // Sla de originele lokale positie van de camera op
        originalLocalPosition = transform.localPosition;
        currentCollisionOffset = maxCollisionOffset; // Begin met de maximale offset
    }

    void Update()
    {
        if (isColliding)
        {
            // Dynamisch de offset berekenen op basis van de afstand tot de muur
            RaycastHit hit;
            if (Physics.Raycast(player.position, -player.forward, out hit, maxCollisionOffset))
            {
                // Bereken de nieuwe offset op basis van de afstand tot de muur
                float distanceToWall = hit.distance;
                currentCollisionOffset = Mathf.Clamp(distanceToWall, minCollisionOffset, maxCollisionOffset);
            }

            // Beweeg de camera dichter naar de speler toe wanneer er botsing is
            Vector3 targetPosition = player.position - player.forward * currentCollisionOffset;

            // Zet de hoogte van de camera op het juiste niveau
            targetPosition.y = player.position.y + heightOffset; // Voeg de hoogte-offset toe
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
        else
        {
            // Herstel de originele positie wanneer er geen botsing meer is
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalLocalPosition, smoothSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall")) // Controleer of de camera de muur raakt
        {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            isColliding = false;
        }
    }
}
