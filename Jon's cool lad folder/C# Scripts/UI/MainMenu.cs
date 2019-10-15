using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour {

    public MainMenuOption mmOption;

    public SceneStorage sceneStorage;

    void Awake()
    {
        mmOption = GetComponent<MainMenuOption>();
    }

    public void NewGame()
    {
        mmOption.menuTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("HackerMan");
    }

    public void LoadGame()
    {
        if (sceneStorage.aSaveExists)
        {
            mmOption.menuTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            sceneStorage.loadedFromMainMenu = true;
            SceneManager.LoadScene("HackerMan");
        }
    }

public void Quit()
    {
        Application.Quit();
        Debug.Log("Application quits when built");
    }
}
