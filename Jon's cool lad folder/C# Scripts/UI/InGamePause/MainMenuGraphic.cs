﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGraphic : MonoBehaviour
{

    public Dropdown ResolutionDropdown;

    Resolution[] resolutions;

    Screen[] fullScreen;

    void Start()
    {
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

  //  public void SetQuality(int qualityIndex)
  //  {
  //      QualitySettings.SetQualityLevel(qualityIndex);
  //  }
}