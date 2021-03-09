using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject player;
    public GameObject[] coins;
    public GameObject gameUI;
    public GameObject mainMenu;
    public CameraFollow cameraFollow;
    public AudioManager audioManager;


   //grabbing Go and UI and turning them off
    void Awake()
    {
        player.SetActive(false);
        coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        coin.SetActive(false);

        gameUI.SetActive(false);
        cameraFollow.enabled = false;
    }
    //starting Background music
    public void Start()
    {
        audioManager.Play("Music");
    }

    //When Activated, turns everything back on and starts the game, turns menu off
    public void PlayGame()
    {
        audioManager.Play("Click");
        mainMenu.SetActive(false);
        player.SetActive(true);
        gameUI.SetActive(true);
        cameraFollow.enabled =true;
        foreach (GameObject coin in coins)
            coin.SetActive(true);
    }

    public void Quit()
    {
        audioManager.Play("Click");
        Application.Quit();
    }
}
