using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour
{
    public AudioManager audioManager;
    public CoinCollectUI cUI;

    public void Start()
    {
        //finding CoinUi in the scene
        cUI = GameObject.FindGameObjectWithTag("CoinUI").GetComponent<CoinCollectUI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the player interacts with the coin, update the coinUI, play SFX and destroy
        if (collision.gameObject.CompareTag("Player"))
        {
            cUI.UpdateCoinCounter();
            audioManager.Play("Coin");
            Destroy(this.gameObject);
        }
    }
}
