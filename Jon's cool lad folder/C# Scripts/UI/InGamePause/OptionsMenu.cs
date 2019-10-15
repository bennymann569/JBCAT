using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour

{
    public GameController controller;

    public Dropdown ResolutionDropdown;
    Resolution[] resolutions;

    public ColorPicker colourPicker;

    public TMP_Text[] textMeshPros;

    #region Audio variables

    //Master Volume
    public Slider masterSlider;

    //Typing Variables
    public AudioSource keySound;
    public Slider keySlider;

    //Music Variables
    public Slider musicSlider;

    bool settingAudioLevels = true;
    #endregion

    private void Awake()
    {
        controller = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Start()
    {
        settingAudioLevels = true;
        masterSlider.value = controller.roomNavigation.masterVolume;
        musicSlider.value = controller.roomNavigation.musicVolume;
        keySlider.value = controller.roomNavigation.keyVolume;
        settingAudioLevels = false;

        Debug.Log("Master slider:" + controller.roomNavigation.masterVolume);
        Debug.Log("Music slider:" + controller.roomNavigation.musicVolume);
        Debug.Log("Key slider:" + controller.roomNavigation.keyVolume);

        resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        {
            colourPicker.onValueChanged.AddListener(color =>
            {
                for (int n = 0; n < textMeshPros.Length; n++)
                {
                    textMeshPros[n].color = colourPicker.CurrentColor;
                }
            });
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void OnValueChanged()
    {
        if (!settingAudioLevels)
        {
            controller.roomNavigation.masterVolume = masterSlider.value;

            if (!controller.hacking)
                controller.roomNavigation.defaultTrack.setVolume(musicSlider.value * masterSlider.value);
            else
                HackingController.hackingTrack.setVolume(musicSlider.value * masterSlider.value);

            keySound.volume = keySlider.value * masterSlider.value;


            controller.roomNavigation.musicVolume = musicSlider.value;
            controller.interactableItems.hackingVolume = musicSlider.value;
            controller.roomNavigation.keyVolume = keySlider.value;
        }
    }
}
