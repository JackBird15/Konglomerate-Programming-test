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

    
    void Start()
    {
        amount = GetComponentInChildren<TMP_Text>();
        coins = GameObject.FindGameObjectsWithTag("Coin");
        //collecting all the coins in the scene
        //we can add as many coins as we want without having to come back to the script and editing it
        foreach (GameObject coin in coins)
        {
            maxCoins++;
        }
        amount.text = ":" + currentCoins + "/" + maxCoins;
    }

    //when updated, it adds 1 to the UI 
    public void UpdateCoinCounter()
    {
        currentCoins++;
        amount.text = ":" + currentCoins + "/" + maxCoins;
    }
}
