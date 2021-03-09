using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public AudioManager audioManager;
    public CoinCollectUI cUI;

    public void Start()
    {
        cUI = GameObject.FindGameObjectWithTag("CoinUI").GetComponent<CoinCollectUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cUI.UpdateCoinCounter();
            audioManager.Play("Coin");
            Destroy(this.gameObject);
        }
    }
}
