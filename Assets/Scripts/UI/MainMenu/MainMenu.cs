using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject mainMenu, optionsMenu, title, credits;
    public Image soundImg;
    public Sprite soundIconOn, soundIconOff;
    bool hasSound;

    void Start()
    {
        hasSound = true;
        InitMenu();
    }
    public void InitMenu()
    {
        title.SetActive(true);
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        credits.SetActive(false);
    }
    public void Options()
    {
        title.SetActive(true);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        credits.SetActive(false);
    }
    public void Credits()
    {
        title.SetActive(false);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        credits.SetActive(true);
    }
    public void SoundToggle()
    {
        hasSound = !hasSound;
        soundImg.sprite = hasSound ? soundIconOn : soundIconOff;
    }
    public void Play()
    {

    }
    public void Stop()
    {
        Application.Quit();
    }
}
