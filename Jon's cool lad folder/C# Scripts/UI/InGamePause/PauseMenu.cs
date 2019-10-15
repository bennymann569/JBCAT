using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    public SceneStorage sceneBoi;

    public GameObject pauseMenuUI;
    public GameObject graphicMenuUI;
    public GameObject optionsMenuUI;
    public GameObject volumeMenuUI;
    public GameObject HelpUI;
    public GameObject GamePlayUI;

    public GameController controller;
    public TextInput textInput;

    public NotepadPanel notepadPanel;
    public SaveLoadManager saveLoad;

    private void Awake()
    {

        notepadPanel = GetComponent<NotepadPanel>();

        gameIsPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenuUI.activeSelf)
            {
                closeOptions();
            }

            else if (graphicMenuUI.activeSelf)
            {
                closeGraphics();
            }

            else if (volumeMenuUI.activeSelf)
            {
                closeVolume();
            }

            else if (HelpUI.activeSelf)
            {
                closeHelp();
            }

            else if (GamePlayUI.activeSelf)
            {
                closeGamePlay();
            }

            else if (gameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        gameIsPaused = true;

        if (notepadPanel.notepad.activeSelf)
        {
            notepadPanel.notepadTextField.DeactivateInputField();
            notepadPanel.notepadTextField.interactable = false;
        }

        else
        {
            textInput.currentField.DeactivateInputField();
            textInput.currentField.interactable = false;
        }


        controller.uiFader.FadeInPanel(pauseMenuUI);
    }

    public void Resume()
    {

        gameIsPaused = false;

        if (notepadPanel.notepad.activeSelf)
        {
            notepadPanel.notepadTextField.interactable = true;
            notepadPanel.notepadTextField.ActivateInputField();
        }
        else
        {
            textInput.currentField.interactable = true;
            textInput.currentField.ActivateInputField();
        }

        controller.uiFader.FadeOutPanel(pauseMenuUI);
    }

    public void SaveGame()
    {
        saveLoad.SaveGame();
        Resume();
    }

    public void LoadGame()
    {
        saveLoad.LoadGame();
        Resume();
    }

    public void openOptions()
    {
        controller.uiFader.FadeOutPanel(pauseMenuUI);

        pauseMenuUI.SetActive(false); // False = controller.uiFader.FadeOutPanel(X); X = the panel you want (pauseMenuUI)

        controller.uiFader.FadeInPanel(optionsMenuUI);

        optionsMenuUI.SetActive(true); // True = controller.uiFader.FadeInPanel(Y);  Y = the panel you want (optionsMenuUI)

    }

    public void closeOptions()
    {
        controller.uiFader.FadeInPanel(pauseMenuUI);

        pauseMenuUI.SetActive(true);

        controller.uiFader.FadeOutPanel(optionsMenuUI);

        optionsMenuUI.SetActive(false);
    }

    public void openGraphics()
    {
        controller.uiFader.FadeOutPanel(optionsMenuUI);

        optionsMenuUI.SetActive(false);

        controller.uiFader.FadeInPanel(graphicMenuUI);

        graphicMenuUI.SetActive(true);
    }

    public void closeGraphics()
    {
        controller.uiFader.FadeOutPanel(graphicMenuUI);

        graphicMenuUI.SetActive(false);

        controller.uiFader.FadeInPanel(optionsMenuUI);

        optionsMenuUI.SetActive(true);
    }

    public void openVolume()
    {
        controller.uiFader.FadeOutPanel(optionsMenuUI);

        optionsMenuUI.SetActive(false);

        controller.uiFader.FadeInPanel(volumeMenuUI);

        volumeMenuUI.SetActive(true);
    }

    public void closeVolume()
    {
        controller.uiFader.FadeOutPanel(volumeMenuUI);

        volumeMenuUI.SetActive(false);

        controller.uiFader.FadeInPanel(optionsMenuUI);

        optionsMenuUI.SetActive(true);
    }

    public void openHelp()
    {
        Pause();

        controller.uiFader.FadeOutPanel(pauseMenuUI);

        pauseMenuUI.SetActive(false);

        controller.uiFader.FadeInPanel(HelpUI);

        HelpUI.SetActive(true);
    }

    public void closeHelp()
    {
        controller.uiFader.FadeOutPanel(HelpUI);

        HelpUI.SetActive(false);

        controller.uiFader.FadeInPanel(pauseMenuUI);

        pauseMenuUI.SetActive(true);
    }

    public void closeGamePlay()
    {
        controller.uiFader.FadeOutPanel(GamePlayUI);

        GamePlayUI.SetActive(false);

        controller.uiFader.FadeInPanel(optionsMenuUI);

        optionsMenuUI.SetActive(true);
    }

    public void openGamePlay()
    {
        controller.uiFader.FadeOutPanel(optionsMenuUI);

        optionsMenuUI.SetActive(false);

        controller.uiFader.FadeInPanel(GamePlayUI);

        GamePlayUI.SetActive(true);
    }

    public void MainMenu()
    {
        Resume();

        sceneBoi.SaveVolumeLevels();

        HackingController.hackingTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        controller.roomNavigation.defaultTrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene("MainMenu");
    }
}