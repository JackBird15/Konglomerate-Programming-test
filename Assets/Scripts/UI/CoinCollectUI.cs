using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollectUI : MonoBehaviour
{
    public GameObject[] coins;
    public float maxCoins;
    public float currentCoins;
    TMP_Text amount;

    
   private void OnEnable()
    {
        CoinCollect.UpdateCoinUi += UpdateCoinCounter;
        amount = GetComponentInChildren<TMP_Text>();
        //collecting all the coins in the scene
        //we can add as many coins as we want without having to come back to the script and editing it
        coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            maxCoins++;
        }
        amount.text = ":" + currentCoins + "/" + maxCoins;
    }

    //when updated, it adds 1 to the UI 
    private void UpdateCoinCounter()
    {
        currentCoins++;
        amount.text = ":" + currentCoins + "/" + maxCoins;
    }
}
