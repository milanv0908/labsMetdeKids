using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AislesAmount : MonoBehaviour
{
    public PlayerCounting[] script; // Array van speler scripts
    public float[] aislePrices; // Array voor prijzen van de schappen

    public TextMeshProUGUI textp1, textp2;

    void Start()
    {
        aislePrices = new float[] { 12f, 25f, 9f, 15f, 22f, 18f }; // Dit zijn de prijzen van de schappen s1 t/m s6

        for (int i = 0; i < aislePrices.Length; i++)
        {
            Debug.Log("Prijs voor schap " + (i + 1) + ": " + aislePrices[i]);
        }
    }

    // Methode om de prijs voor een specifiek schap op te halen
    public float GetPriceForAisle(string aisleTag)
    {
        switch (aisleTag)
        {
            case "s1":
                return aislePrices[0];
            case "s2":
                return aislePrices[1];
            case "s3":
                return aislePrices[2];
            case "s4":
                return aislePrices[3];
            case "s5":
                return aislePrices[4];
            case "s6":
                return aislePrices[5];
            default:
                Debug.LogError("Onbekende schap tag: " + aisleTag);
                return 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("p1") || other.CompareTag("p2"))
        {
           string aisleTag = gameObject.tag; //haal het schap op
          float price = GetPriceForAisle(aisleTag); // haal de prijs op

          foreach (PlayerCounting script in script)
          {
             script.purchaseAmount = price; // Verander de purchaseAmount in script
             script.isInTrigger = true; // Zet isInTrigger naar true
          }

        if (other.CompareTag("p1"))
            {
                textp1.gameObject.SetActive(true);  // Maak textp1 zichtbaar
                textp1.text = "Prijs voor dit item is " + ": €" + price;
            }
        else if (other.CompareTag("p2"))
            {
                textp2.gameObject.SetActive(true);  // Maak textp2 zichtbaar
                textp2.text = "Prijs voor dit item is " + ": €" + price;
            }
        
             Debug.Log("De prijs voor schap " + aisleTag + " is: " + price);
         }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("p1") || other.CompareTag("p2"))
        {
            foreach (PlayerCounting script in script)
            {
                script.purchaseAmount = 0f; // Reset de purchaseAmount in script
                script.isInTrigger = false; // Zet isInTrigger naar false
            }

            if (other.CompareTag("p1"))
                {
                    textp1.gameObject.SetActive(false);  // Maak textp1 zichtbaar
                }
            else if (other.CompareTag("p2"))
                {
                    textp2.gameObject.SetActive(false);  // Maak textp2 zichtbaar
                }
        }
    }
}
