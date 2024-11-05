using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletManager : MonoBehaviour
{

    public int maxBudget = 120;
    public TextMeshProUGUI textp1, textp2;
    public static float TotalMoney = 0;

    // Functie om geld toe te voegen aan de gezamenlijke portemonnee
    public static void AddMoney(float amount)
    {
        TotalMoney += amount;
    }

    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("p1")) {
        // Update de tekst voor speler 1
            if (TotalMoney < maxBudget) {
                textp1.text = "Jullie zitten nog onder het maximum budget.";
            } else if (TotalMoney > maxBudget) {
                textp1.text = "Jullie zitten boven het maximum budget.";
            } else if (TotalMoney == maxBudget) {
                textp1.text = "Jullie zitten op het maximum budget.";
            }

            textp1.gameObject.SetActive(true); // Maak de tekst zichtbaar voor speler 1
        }

        if (other.CompareTag("p2")) {
        // Update de tekst voor speler 2
            if (TotalMoney < maxBudget) {
                textp2.text = "Jullie zitten nog onder het maximum budget.";
            } else if (TotalMoney > maxBudget) {
                textp2.text = "Jullie zitten boven het maximum budget.";
            } else if (TotalMoney == maxBudget) {
                textp2.text = "Jullie zitten op het maximum budget.";
            }
        textp2.gameObject.SetActive(true); // Maak de tekst zichtbaar voor speler 2
        }
    }


    private void OnTriggerExit(Collider other){
        if (other.CompareTag("p1")){
            textp1.gameObject.SetActive(false); // Maak de tekst voor speler 1 niet meer zichtbaar
        }
        if (other.CompareTag("p2")){
            textp2.gameObject.SetActive(false); // Maak de tekst voor speler 2 niet meer zichtbaar
        }
    }
}