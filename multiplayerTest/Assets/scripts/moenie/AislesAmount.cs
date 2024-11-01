using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AislesAmount : MonoBehaviour
{
    public PlayerCounting[] script; // Array van speler scripts
    public float[] aislePrices; // Array voor prijzen van de schappen

    void Start()
    {
        aislePrices = new float[] { 10f, 20f, 30f, 40f, 50f, 60f }; // Dit zijn de prijzen van de schappen s1 t/m s6

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
        }
    }
}
