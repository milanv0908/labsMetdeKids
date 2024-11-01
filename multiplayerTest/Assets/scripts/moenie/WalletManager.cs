using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : MonoBehaviour
{
    public static float TotalMoney = 0;

    // Functie om geld toe te voegen aan de gezamenlijke portemonnee
    public static void AddMoney(float amount)
    {
        TotalMoney += amount;
    }
}