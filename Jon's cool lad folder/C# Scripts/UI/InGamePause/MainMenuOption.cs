using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuOption : MonoBehaviour

{
    public GameObject graphicMenuUI;
    public GameObject optionsMenuUI;
    public GameObject volumeMenuUI;

    public SceneStorage sceneStorage;

    public Slider volumeSlider;

    public FMOD.Studio.EventInstance menuTrack;

    void Start()
    {
        menuTrack = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Main_Menu_Theme");
        menuTrack.start();
    }

    public void openOptions()
    {
        optionsMenuUI.SetActive(true);
    }

    public void closeOptions()
    {

        optionsMenuUI.SetActive(false);
    }

    public void openGraphics()
    {
        optionsMenuUI.SetActive(false);

        graphicMenuUI.SetActive(true);
    }

    public void closeGraphics()
    {
        graphicMenuUI.SetActive(false);

        optionsMenuUI.SetActive(true);
    }

    public void openVolume()
    {
        optionsMenuUI.SetActive(false);

        volumeMenuUI.SetActive(true);
    }

    public void closeVolume()
    {
        volumeMenuUI.SetActive(false);

        optionsMenuUI.SetActive(true);
    }

    public void OnValueChanged()
    {
        menuTrack.setVolume(volumeSlider.value);
        sceneStorage.mainMenuVolume = volumeSlider.value;
    }
}