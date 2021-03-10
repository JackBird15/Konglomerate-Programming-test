using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinCollect : MonoBehaviour
{
    public static event Action UpdateCoinUi;
    public static event Action<string> PlayAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player interacts with the coin, update the coinUI, play SFX and destroy
        if (collision.gameObject.CompareTag("Player"))
        {
            UpdateCoinUi?.Invoke();
            PlayAudio?.Invoke("Coin");
            Destroy(this.gameObject);
        }
    }
}
