using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject Player;
    public GameObject UiBar;
    public GameObject MainMenu;
    public CameraFollow cameraFollow;
    public AudioManager audioManager;


    // Start is called before the first frame update
    void Awake()
    {
        Player.SetActive(false);
        UiBar.SetActive(false);
        cameraFollow.enabled = false;
    }

    public void Start()
    {
        audioManager.Play("Music");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        audioManager.Play("Click");
        MainMenu.SetActive(false);
        Player.SetActive(true);
        UiBar.SetActive(true);
        cameraFollow.enabled =true;
    }

    public void Quit()
    {
        audioManager.Play("Click");
        Application.Quit();
    }
}
