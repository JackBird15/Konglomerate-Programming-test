using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public AudioManager audioManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioManager.Play("Coin");
        Destroy(this.gameObject);
    }
}
