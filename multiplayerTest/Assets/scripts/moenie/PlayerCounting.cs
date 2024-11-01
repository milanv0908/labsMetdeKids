using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounting : MonoBehaviour
{
    public float MoneyPlayerOne, MoneyPlayerTwo;
    public float purchaseAmount = 10f; // Variabel aankoopbedrag, pas dit aan wanneer een item geselecteerd wordt
    private float discountP1 = 0.8f;    // 20% korting voor p1
    private float discountP2 = 5f;      // €5 korting voor p2
     public bool isInTrigger = false;

    void Start()
    {
        MoneyPlayerOne = 0;
        MoneyPlayerTwo = 0;
        WalletManager.TotalMoney = 0; // Zorg dat de gezamenlijke wallet op 0 begint
    }

    void Update()
    {
         if (isInTrigger) // Alleen doorgaan als de speler in de trigger is
             {
                 if (tag == "p1" && Input.GetKeyDown("k"))
                    {
                         // Pas de 20% korting toe voor speler 1
                         float discountedAmountP1 = purchaseAmount * discountP1;
                         MoneyPlayerOne += discountedAmountP1;
                         WalletManager.AddMoney(discountedAmountP1);

                          Debug.Log("p1's Money After Discounted Addition: " + MoneyPlayerOne);
                     }

                     if (tag == "p2" && Input.GetKeyDown(KeyCode.JoystickButton1))
                         {
                             // Pas de €5 korting toe voor speler 2, zonder dat het onder €0 komt
                             float discountedAmountP2 = purchaseAmount - discountP2;
                              if (discountedAmountP2 > 0) // Zorg ervoor dat het niet onder €0 gaat
                                  {
                                    MoneyPlayerTwo += discountedAmountP2;
                                    WalletManager.AddMoney(discountedAmountP2);
                                  }
                         }
             }

         Debug.Log("Total Money in Shared Wallet: " + WalletManager.TotalMoney);
    }
}
