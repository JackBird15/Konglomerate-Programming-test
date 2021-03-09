using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollectUI : MonoBehaviour
{
    public GameObject[] Coins;
    public float maxCoins;
    public float currentCoins;
    TMP_Text amount;

    void Start()
    {
        amount = GetComponentInChildren<TMP_Text>();
        Coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach(GameObject coin in Coins)
        {
            maxCoins++;
        }
        amount.text = ":" + currentCoins + "/" + maxCoins;
    }

    public void UpdateCoinCounter()
    {
        currentCoins++;
        amount.text = ":" + currentCoins + "/" + maxCoins;
    }
}
