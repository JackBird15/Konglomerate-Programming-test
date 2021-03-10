using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Menu : MonoBehaviour
{
    public static event Action<string> PlayAudio;

    public GameObject player;
    public GameObject[] coins;
    public GameObject gameUI;
    public GameObject mainMenu;
    public CameraFollow cameraFollow;
    public AudioManager audioManager;


   //grabbing Go and UI and turning them off
    private void Awake()
    {
        player.SetActive(false);
        gameUI.SetActive(false);
        cameraFollow.enabled = false;
    }
    //starting Background music
    private void Start()
    {
        PlayAudio?.Invoke("Music"); 
    }

    //When Activated, turns everything back on and starts the game, turns menu off
    private void PlayGame()
    {
        PlayAudio?.Invoke("Click");
        mainMenu.SetActive(false);
        player.SetActive(true);
        cameraFollow.enabled =true;
        gameUI.SetActive(true);
    }

    private void Quit()
    {
        PlayAudio?.Invoke("Click");
        Application.Quit();
    }
}
